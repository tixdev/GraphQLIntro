# Industrializzazione Graph API

Questo documento definisce lo standard per la creazione di grafi, tipi, estensioni e DataLoader all'interno dei microservizi.

## 1. Struttura delle Cartelle
Ogni microservizio deve organizzare la cartella `Graph` come segue:
- `Graph/Types/`: Classi `ObjectType<T>` per le entità proprietarie del servizio.
- `Graph/Extensions/`: Estensioni di tipi appartenenti ad altri servizi (Federation).
- `Graph/DataLoaders/`: Implementazioni individuali di `BatchDataLoader` o `GroupedDataLoader`.

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
    *   **Azione:** Crea un'estensione del tipo.
    *   **File:** 📂 `Graph/Extensions/[External]Extensions.cs`
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
3.  **Tipo/Estensione**: Crea i tipi `Graph/Types/` (mettendo `@key` e `ResolveReferenceWith`) o le estensioni `Graph/Extensions/` (usando `[ObjectType]` e `[Key]`).
