# Industrializzazione Graph API

Questo documento definisce lo standard per la creazione di grafi, tipi, estensioni e DataLoader all'interno dei microservizi.

## 1. Struttura delle Cartelle
Ogni microservizio deve organizzare la cartella `Graph` come segue:
- `Graph/Types/`: Classi `ObjectType<T>` per le entità proprietarie del servizio.
- `Graph/TypeExtensions/`: Estensioni di tipi locali per aggiungere campi o riferimenti verso altri servizi (es. risoluzione di Federation Stubs).
- `Graph/ExternalTypeRefs/`: Estensioni di tipi appartenenti ad altri servizi (Federation Reference).
- `Graph/DataLoaders/`: Implementazioni individuali di `BatchDataLoader` o `GroupedDataLoader`.
- `Graph/FederationStubs/`: Classi DTO vuote (Stub) per la risoluzione di riferimenti esterni.

## 2. Decision Framework: Le "Domande Giuste"
1.  **Ownership**: Chi "possiede" questo dato? (es. `Relationship.API` possiede le relazioni).
2.  **Entry Point**: Da dove voglio scoprire questo dato? (es. Da `Person` voglio vedere le sue `Relationships`? Sì -> Extension su `Person`).
3.  **Astrazione**: Devo esporre la tabella di join o l'entità logica? (es. meglio `Person.relationships` che `Person.relationshipToPerson`).
4.  **Navigabilità**: Posso andare più in profondità? (es. da `Relationship` voglio vedere gli `Assets`?).

## 3. Scelta del DataLoader

| Tipo | Quando Usarlo | Esempio |
| :--- | :--- | :--- |
| **BatchDataLoader<K, V>** | **Lookup per Chiave (1:1)**. Quando hai un ID e vuoi l'entità corrispondente. | `RelationshipByIdDataLoader` |
| **GroupedDataLoader<K, V>** | **Relazioni (1:N)**. Quando hai un ID (es. `PersonID`) e vuoi una *collezione* di entità correlate (es. `List<Relationship>`). | `RelationshipsByPersonIdDataLoader` |

> [!TIP]
> Il `GroupedDataLoader` è preferibile per le collezioni perché gestisce automaticamente il raggruppamento delle chiavi duplicate e restituisce una lista per ogni chiave.

---

## 4. Decision Flow (Albero Decisionale)

Usa questa guida per capire esattamente **quali file** creare e **che tipo** di oggetti usare.

### Cosa stai esponendo?

1.  **Sto esponendo una Nuova Entità del MIO dominio** (es. `Asset`, `Relationship`)
    *   **Azione:** Crea il tipo base.
    *   **File:** 📂 `Graph/Types/[Entity]Type.cs`
    *   *Come la recupero per ID (per la Federation `@key`)?*
        *   **Azione:** Crea un DataLoader per chiave primaria.
        *   **File:** 📂 `Graph/DataLoaders/[Entity]ByIdDataLoader.cs`
        *   **Eredita da:** `BatchDataLoader<int, [Entity]>`
2.  **Sto aggiungendo un campo a un'entità ESTERNA** (es. partendo da `Person` voglio mostrare le sue `Relationships`)
    *   **Azione:** Crea un'estensione del tipo remoto tramite la tecnica del Reference.
    *   **File:** 📂 `Graph/ExternalTypeRefs/[External]Ref.cs`
    *   *Che cardinalità ha la relazione che sto aggiungendo?*
        *   **Caso A: È una relazione 1 a 1** (es. un `Asset` ha 1 `Balance`)
            *   **Azione:** Crea un DataLoader che torna un singolo oggetto.
            *   **File:** 📂 `Graph/DataLoaders/[Target]By[Source]IdDataLoader.cs`
            *   **Eredita da:** `BatchDataLoader<int, [Target]>`
        *   **Caso B: È una relazione 1 a N** (es. una `Person` ha N `Relationships`)
            *   **Azione:** Crea un DataLoader che torna una collezione.
            *   **File:** 📂 `Graph/DataLoaders/[Target]sBy[Source]IdDataLoader.cs`
            *   **Eredita da:** `GroupedDataLoader<int, [Target]>`

---

## 5. Workflow Pragmatico di Implementazione
1.  **Segui il Flow**: Usa il diagramma sopra per decidere cosa ti serve.
2.  **Parti dal DataLoader**: Crea sempre prima i loader in `Graph/DataLoaders/`.
3.  **Tipo/Estensione**: Crea i tipi `Graph/Types/` (mettendo `@key` e `ResolveReferenceWith`) oppure le estensioni locali in `Graph/TypeExtensions/` e le esterne in `Graph/ExternalTypeRefs/`.

---

## 6. Registrazione Tipi ed Estensioni (AutoScaffolding)

Per mantenere il codice pulito e omogeneo, **tutti i microservizi DEVONO** utilizzare la convention dell'*auto-scaffolding* per registrare i propri tipi ed estensioni GraphQL all'interno del file di setup (`Extensions/GraphQLExtensions.cs`).

Invece di chiamare ripetutamente `.AddTypeExtension<...>()` manuali:
1. Crea/assicurati che esista il file `Extensions/AutoScaffoldExtensions.cs` contenente il metodo di estensione `AddAutoScaffoldedTypes()`.
2. Chiama `.AddAutoScaffoldedTypes()` sulla configurazione GraphQL Server (di solito subito dopo la registrazione della query).

Questa convenzione garantisce che nessun tipo configurato o aggiornato venga per errore escluso dal grafo federato.

---

## 7. Paginazione delle Relazioni: La regola d'oro

Quando si impagina una collezione (es. `Person.relationships(take: 10)`), sorge spesso il dubbio se paginare lato database con `IQueryable` o in memoria con un `DataLoader`.

**Regola:** Usa SEMPRE il `DataLoader` combinato con `[UseOffsetPaging]`.

### Perché NON usare IQueryable direttamente dal parent?
Restituire un `IQueryable` (decorato con `[UseOffsetPaging]`) da un resolver figlio genera un disastroso **problema N+1** in architetture a grafo federato.
Se il resolver parent (`Person`) restituisce 50 risultati, HotChocolate eseguirà **50 query SQL separate** alla tabella `Relationship`, sebbene paginate individualmente (`TOP(10)`).

### La soluzione standardizzata ( DataLoader + Paging In-Memory )

```csharp
[UseOffsetPaging(IncludeTotalCount = true)] // 2. Applica la paginazione in memoria
public async Task<IEnumerable<RelationshipModel>> GetRelationships(
    [Parent] PersonExtensions person,
    RelationshipsByPersonIdDataLoader dataLoader) // 1. Usa SEMPRE il DataLoader
{
    var results = await dataLoader.LoadAsync(person.PersonID);
    return results ?? Array.Empty<RelationshipModel>();
}
```
**Perché funziona:**
1. Il `RelationshipsByPersonIdDataLoader` raggrupperà tutti i 50 `PersonID` ed eseguirà **una singola query SQL** (`WHERE PersonID IN (...)`).
2. HotChocolate prenderà il risultato in memoria (`IEnumerable`) e applicherà i cursori/offset previsti da GraphQL (`skip`, `take`) prima di inviare i dati al client.

*Nota:* Finché la cardinalità massima di una singola relazione non supera l'ordine di grandezza dei 10.000 record per parent, il caricamento in memoria C# seguito da un subset `Take()` è nettamente più performante di 50 o 100 query SQL separate sul database.

---

## 8. Convenzioni Nomi e Federation Stubs

Quando si estende un tipo GraphQL per aggiungere un campo verso un altro **subgraph** (Apollo Federation), è fondamentale rispettare la seguente struttura e nomenclatura:

1. **Estensione Fortemente Tipizzata**:
   Usa sempre `[ExtendObjectType(typeof(Model))]` passando il tipo C# invece di una "magic string" (es. `[ExtendObjectType("GroupPerson")]`). Questo garantisce il binding corretto a runtime.

2. **Classe Contenitore**:
   Il nome della classe dovrebbe indicare quale modello si sta estendendo e il contesto (es. `GroupPersonPlausibilityExtensions`). HotChocolate capisce l'estensione tramite l'attributo, il nome C# è solo per organizzazione.

3. **Nome del Metodo**:
   Si usa il prefisso `Get` (es. `GetJoinType`). HotChocolate per default lo rimuoverà, esponendo il campo GraphQL col nome `joinType`.

4. **Federation Stub**:
   Se il record non appartiene a questo subgraph e abbiamo solo un ID, la classe di ritorno deve essere denominata col suffisso `FederationStub` (es. `JoinTypeFederationStub`). Questo indica a tutti gli sviluppatori C# che è un segnaposto contenente solo un ID o puntatore esterno.

5. **Attributo GraphQLName sullo Stub**:
   Sul DTO dello stub, usa `[GraphQLName("NomeReale")]` (es. `[GraphQLName("JoinType")]`). In questo modo l'uso interno C# del nome *Stub* scompare dallo schema GraphQL, e l'Apollo Router lo riconoscerà per federarlo col subgraph corrispondente.

6. **Struttura dei File ("One File Per Stub")**:
   Tutti i Federation Stubs devono essere creati all'interno della cartella dedicata `Graph/FederationStubs/` all'interno del rispettivo microservizio. Poiché si tratta di file molto piccoli, è obbligatorio creare **un file per ogni tipo di riferimento esterno** (es. `JoinTypeFederationStub.cs`), evitando di creare file monolitici con centinaia di classi o decine di file inutilmente complessi raggruppati assieme. In questo modo si garantisce modularità e facile ricercabilità.

---
*(Ultimo aggiornamento: Implementazione di convenzioni per Federation Stubs ed ExtendObjectType tipizzato)*

## 9. Riferimenti a Entità Esterne (Federation Reference)

Quando un microservizio deve aggiungere campi o collezioni a un'entità "posseduta" da un **altro** microservizio (es. `Relationship.API` vuole aggiungere la lista delle relazioni all'entità esterna `Person`), **NON usare `ExtendObjectType`**.

Per evitare errori di `INVALID_FIELD_SHARING` durante la composizione del Supergraph (Apollo Federation), è obbligatorio usare il cosiddetto **Reference Pattern** in un file separato (es. `PersonRef.cs` all'interno della cartella `Graph/ExternalTypeRefs`):

1.  **Definizione del Tipo tramite stringa**: Usa `[ObjectType("EntityName")]` in modo che il Gateway sappia quale tipo esatto GraphQL andare ad estendere.
2.  **Definizione della Chiave**: Usa `[HotChocolate.ApolloFederation.Types.Key("entityID")]` per definire l'ID primario (deve coincidere col campo `@key` dell'owner).
3.  **Classe "Ref"**: La classe C# **DEVE** avere il suffisso `Ref` (es. `PersonRef`). Questo distingue chiaramente a livello visivo e di codice che si sta manipolando un puntatore a un'entità remota, e non un'estensione locale.
4.  **Proprietà ID pubblica**: Definisci il campo ID `public int EntityID { get; set; }` affinché GraphQL lo valorizzi al momento della risoluzione federata.
5.  **ReferenceResolver**: Un metodo statico decorato con `[ReferenceResolver]` che prende in input l'istanza parziale e la restituisce materializzata in memoria limitatamente al suo ID.

**Esempio corretto (in `Relationship.API/Graph/ExternalTypeRefs/PersonRef.cs`):**
```csharp
[ObjectType("Person")]
[HotChocolate.ApolloFederation.Types.Key("personID")]
public class PersonRef
{
    [ReferenceResolver]
    public static async Task<PersonRef> GetByIdAsync(int personID)
        => await Task.FromResult(new PersonRef { PersonID = personID });

    public int PersonID { get; set; }

    [UseOffsetPaging(DefaultPageSize = 10, MaxPageSize = 200, IncludeTotalCount = true)]
    public async Task<IEnumerable<RelationshipModel>> GetRelationships(
        [Parent] PersonRef person,
        RelationshipsByPersonIdDataLoader dataLoader)
    {
        var results = await dataLoader.LoadAsync(person.PersonID);
        return results ?? Array.Empty<RelationshipModel>();
    }
}
```

Applicando questo design pattern in tutti i subgraphs (vedi analoghi in `Asset.API` e `Balance.API`), Apollo delegherà correttamente la risoluzione dei campi locali a questo microservizio, mentre la definizione "Master" dell'entità risiederà solo nel servizio proprietario.

---

## 10. Proiezioni Entity Framework per Federation Stubs (IsProjected)

Quando si estende un tipo con un **Federation Stub** (es. `GetPersonNature` che restituisce `PersonNatureFederationStub`), il resolver ha bisogno della Foreign Key (es. `PltPersonNatureID`) presente sul modello Entity Framework.

Poiché HotChocolate ottimizza le query SQL proiettando **solo i campi richiesti dal client**, se il client richiede `personNature { code }` ma *non* esplicita `pltPersonNatureID` nella query, Entity Framework restituirà il record genitore con quell'ID impostato al default (es. `0` o `null`). Di conseguenza, il metodo di estensione restituirà `null`.

**Soluzione:** È obbligatorio informare HotChocolate che determinati campi (Foreign Keys) devono essere **sempre estratti dal database**, a prescindere da cosa chiede il client.
Si fa applicando `IsProjected()` nel metodo `Configure` del file `*Type.cs` principale.

**Esempio (in `PersonType.cs`):**
```csharp
public class PersonType : ObjectType<Person.API.Models.Person>
{
    protected override void Configure(IObjectTypeDescriptor<Person.API.Models.Person> descriptor)
    {
        var method = typeof(PersonType).GetMethod(nameof(GetByIdAsync))!;
        descriptor.Key("personID").ResolveReferenceWith(method);

        // FORZA IL FETCH DAL DB DI QUESTE FORGEIGN KEYS:
        descriptor.Field(t => t.PltPersonNatureID).IsProjected();
        descriptor.Field(t => t.PltPersonCodingTypeID).IsProjected();
    }
    // ...
}
```
In questo modo, permettiamo la navigazione GraphQL fluida e indolore, accettando un minimo compromesso di "overfetching" per queste singole colonne quando non richieste direttamente.

---

## 11. GraphQL Performance: Le "Regole d'Oro"

Per garantire che il grafo sia ultra-performante e non sovraccarichi il database, segui queste regole architettoniche consolidate:

### 1. Root Query & Proiezioni (Selettività delle Colonne)
*   **Regola**: Sulla query principale (es. `person`) usa sempre `[UseProjection]`.
*   **Perché**: Assicura che EF Core generi una `SELECT` solo per le colonne scalari richieste (es. `PersonID`, `PersonNumber`), evitando il costoso `SELECT *`.

### 2. Disattivazione Auto-JOIN (`IsProjected(false)`)
*   **Regola**: Tutte le navigation property (1:1 o 1:N) che hanno un resolver/DataLoader dedicato devono essere marcate obbligatoriamente con `.IsProjected(false)` nel descrittore del tipo (`PersonType.cs`).
*   **Perché**: Impedisce a `[UseProjection]` di iniettare dei `LEFT JOIN` nella query root. Senza questo, EF Core scaricherebbe i dati delle relazioni due volte (una nella query root via JOIN e una nel DataLoader via query separata).

### 3. Granularità dei DataLoader (Evitare l'Overfetching del DB)
*   **Regola**: Non creare un unico DataLoader monolitico (es. `PersonById`). Suddividilo in loader granulari per ogni dominio/natura (`NaturalPersonByPersonId`, `InternalPersonByPersonId`, etc.).
*   **Perché**: Se il client chiede solo `internalPerson`, il DB interroga solo la tabella specifica. Questo riduce la complessità dei piani di esecuzione SQL e previene il caricamento di tabelle inutilizzate.

### 4. Risoluzione dell'N+1 via Batching
*   **Regola**: Usa sempre i `DataLoader` per le relazioni.
*   **Perché**: HotChocolate raggruppa gli ID ed esegue **una sola query SQL aggiuntiva** per ogni tipo di relazione richiesta (`WHERE ID IN (...)`), garantendo che il numero di query totali sia costante (`1 + N_Relazioni`) e non proporzionale al numero di record restituiti.

### 5. Ciclo di Vita `Transient` per il DbContext
*   **Regola**: Se il `DbContext` ha dipendenze `Scoped` (es. `ITemporalContext` per filtri temporali via header) ed è usato in DataLoader paralleli, **deve** essere registrato come `ServiceLifetime.Transient`.
*   **Perché**: I DataLoader girano in parallelo. Il ciclo di vita `Transient` garantisce thread-safety totale evitando collisioni sul Context. Il pooling fisico delle connessioni rimane gestito in modo sicuro ed efficiente da ADO.NET.

### 6. Mapping esplicito (`Include`) nei DataLoader
*   **Regola**: All'interno del `LoadBatchAsync` del DataLoader (che opera con `.AsNoTracking()`), usa esplicitamente `.Include()` e `.ThenInclude()` per popolare i dati satellite (es. `SensibleData`).
*   **Perché**: Senza tracking e senza proiezioni lato root, EF Core non popola automaticamente le relazioni annidate. All'interno del loader, l'overfetching verso tabelle strettamente collegate 1:1 è accettabile e consigliato per minimizzare le query.
