# Gestione dei Filtri Temporali (Temporal Filtering)

Questo documento illustra l'architettura adottata per l'implementazione del filtraggio temporale basato sulle intestazioni HTTP (Headers) nelle API GraphQL del progetto.

## Architettura: Global Query Filters

Il dominio richiede che entità storicizzate (come `Person`, `LegalPerson`, ecc.) vengano filtrate in base a coordinate temporali passate tramite header HTTP (`x-temporal-mode`, `x-temporal-range-start`, `x-temporal-range-end`).

La soluzione si basa sull'uso dei **Global Query Filters** nativi di EF Core (`HasQueryFilter`). Viene registrato dinamicamente un filtro su tutte le entità che implementano l'interfaccia `ITemporalEntity` tramite reflection, senza dover modificare a mano ogni singola configurazione nel `DbContext`.

```csharp
// In PersonContext.OnModelCreating
modelBuilder.Entity<TEntity>().HasQueryFilter(e =>
    e.ValidStartDate <= TemporalContext.QueryMaxStartDate &&
    e.ValidEndDate > TemporalContext.QueryMinEndDate);
```

Questo approccio permette a EF Core di generare il piano di esecuzione **una sola volta**, cacharlo, e passargli a runtime `@p0` e `@p1` come semplici parametri, mantenendo le performance di compilazione al massimo livello.

---

## Il "Trucco Matematico" per le Performance SQL

Una sfida posta dai Global Query Filters era la complessità logica dei 4 scenari temporali:
- **AsOf**: Trova i record validi in un istante specifico.
- **AnyTimeIn**: Trova i record che si accavallano con un periodo (Start/End).
- **Throughout**: Trova i record che coprono interamente un periodo (Start/End).
- **All**: Nessun filtro (restituisci tutta la storia).
- **Default (Nessun header)**: Restituisci solo i record "Attivi" oggi.

Se avessimo implementato queste condizioni in un unico Global Filter usando molti `OR` dipendenti da uno switch (`@Mode = 1 OR @Mode = 2...`), SQL Server avrebbe sofferto di **Parameter Sniffing**, disabilitando la possibilità di sfruttare gli indici sulle date e riducendo le query a dei lenti *Table Scans*.

### Semplificazione Universale (No-ORs)

La soluzione adottata è un trucco matematico che riconduce TUTTI e 4 i casi logici a **una singola condizione universale** di intersezione (Bounding Box):

```sql
WHERE ValidStartDate <= @QueryMaxStartDate AND ValidEndDate > @QueryMinEndDate
```

In `TemporalContext.cs`, calcoliamo i confini ottimali (`QueryMaxStartDate` e `QueryMinEndDate`) in C# basandoci sulla modalità richiesta, garantendo a SQL Server un'istruzione di una semplicità estrema e perfettamente indicizzabile.

#### Mappatura dei Casi

| Mode | Input Utente | Calcolo `QueryMaxStartDate` | Calcolo `QueryMinEndDate` | Condizione SQL Risultante (Semplificata) |
|---|---|---|---|---|
| **Default (Active)** | N/A | `DateTime.MaxValue` | `DateTime.MaxValue.AddTicks(-1)` | `EndDate > MaxValue - 1` (cioè `EndDate == MaxValue`) |
| **AsOf** | `X` | `X` | `X` | `StartDate <= X AND EndDate > X` |
| **AnyTimeIn** | `S`, `E` | `E.AddTicks(-1)` | `S` | `StartDate < E AND EndDate > S` |
| **Throughout** | `S`, `E` | `S` | `E.AddTicks(-1)` | `StartDate <= S AND EndDate >= E` |
| **All** | N/A | `DateTime.MaxValue` | `DateTime.MinValue` | `StartDate <= Max AND EndDate > Min` (Sempre vera) |

### Benefici

1. **Zero Ricompilazioni C#**: EF Core genera un solo Service Provider e non butta mai via la cache.
2. **Miglior Execution Plan in SQL Server**: Nessun operatore `OR`, la query sfrutta nativamente l'indice multicolonna (B-Tree) su `(ValidStartDate, ValidEndDate)`. Nessun rischio di Parameter Sniffing perché la forma della query non cambia.
3. **Astrazione Trasparente**: Chi scrive i DataLoader GraphQL non deve ricordarsi di aggiungere clausole `.Where()` o `TagWith()`. Finché l'entità estende `ITemporalEntity`, viene filtrata in totale sicurezza.
