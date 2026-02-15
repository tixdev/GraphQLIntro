# GraphQL — A Modern Approach to APIs

Linee guida e contenuti di riferimento per la presentazione GraphQL.

---

## Struttura della Presentazione

| # | Slide | Messaggio chiave |
|---|-------|-----------------|
| 1 | Titolo | GraphQL — A Modern Approach to APIs |
| 2 | Over-fetching | REST trasferisce 300× più dati del necessario |
| 3 | Under-fetching | 6 chiamate waterfall (865ms) vs 1 query GraphQL (140ms) |
| 4 | Multiple Endpoints | 150+ endpoint REST vs 1 singolo `/graphql` |
| 5 | Vantaggi | Efficienza, performance, DX, flessibilità, type safety, evoluzione |
| 6 | Demo | Invito alla demo live + risorse |

---

## Contenuti Dettagliati per Slide

### Slide 1 — Titolo

Snippet decorativi di sintassi GraphQL per contesto visivo:

```graphql
query {
  user(id: 123) {
    name
    email
    posts { title }
  }
}

fragment Info {
  id
  avatar
}

mutation {
  update(...)
}

type User {
  name: String
}
```

---

### Slide 2 — Problem #1: Over-fetching

**Scenario**: dropdown autocomplete che cerca prodotti.

| | REST | GraphQL |
|---|---|---|
| Dati trasferiti | 85.2 KB | 280 B |
| Latenza | 245 ms | 98 ms |
| Dati usati | ~300 bytes | 280 bytes |
| Rapporto | **~0.3% utile** | **~100% utile** |

**REST** scarica l'intero oggetto (descrizione 2KB, specifiche 15KB, immagini 20KB, recensioni 35KB) quando servono solo `id` e `name`.

**GraphQL** chiede esattamente ciò che serve:

```graphql
{
  products(search: "laptop") {
    id
    name
  }
}
```

> **Talking point**: "Immagina moltiplicare questo spreco per milioni di utenti mobile con connessioni limitate."

---

### Slide 3 — Problem #2: Under-fetching

**Scenario**: dashboard ordini che richiede dati da 6 entità diverse.

**REST — Waterfall sequenziale:**

| Richiesta | Latenza |
|-----------|---------|
| `GET /orders/456` | 120ms |
| `GET /customers/789` | 130ms |
| `GET /products?ids=1,2,3` | 140ms |
| `GET /inventory?ids=1,2,3` | 125ms |
| `GET /shipping/calc` | 135ms |
| `GET /payments/456` | 115ms |
| **Totale** | **~865ms** |

**GraphQL — Singola richiesta:**

```graphql
{
  order(id: 456) {
    id, total
    customer { name, email }
    items {
      product { name, price }
      inventory { available }
    }
    shipping { method, cost }
    payment { status }
  }
}
```

Tempo: **~140ms** — 6× più veloce.

> **Talking point**: "Il server GraphQL risolve tutti i campi in parallelo internamente, eliminando il waterfall."

---

### Slide 4 — Problem #3: Multiple Endpoints

**REST**: proliferazione di endpoint specializzati.

```
GET /api/products
GET /api/products/:id
GET /api/products/search
GET /api/products/autocomplete
GET /api/products/:id/reviews
GET /api/products/:id/inventory
GET /api/products/:id/pricing
GET /api/products/trending
GET /api/products/recommendations
// ... 150+ endpoint
```

→ Incubo di manutenzione e versioning (`/v1/`, `/v2/`, `/v3/`...).

**GraphQL**: un singolo endpoint `/graphql`, i client compongono le query. Lo schema è **auto-documentante** tramite introspection.

> **Talking point**: "Ogni nuova vista nel frontend spesso richiede un nuovo endpoint REST. Con GraphQL il frontend è autonomo."

---

### Slide 5 — The GraphQL Advantage

| Icona | Vantaggio | Dettaglio |
|-------|-----------|-----------|
| ⚡ | **Efficiency** | 300× meno dati trasferiti |
| 🚀 | **Performance** | 6× page load più veloce |
| 🛠️ | **Developer Experience** | API auto-documentanti |
| 🎯 | **Flexibility** | I client definiscono i propri bisogni di dati |
| ✅ | **Type Safety** | Schema fortemente tipizzato |
| ♻️ | **Evolution** | Aggiunta campi senza versioning |

---

### Slide 6 — Demo

Invito alla demo live con risorse di riferimento:

- [graphql.org](https://graphql.org) — Specifica ufficiale e tutorial
- [Apollo GraphQL](https://www.apollographql.com) — Client/Server ecosystem
- [The Guild](https://the-guild.dev) — Tooling open-source (GraphQL Code Generator, Yoga, ecc.)

---

## Argomenti da Aggiungere (Suggerimenti)

### 🔰 Cos'è GraphQL — Slide introduttiva

Contesto storico e definizione:

- **Inventato da Facebook** nel 2012 per le esigenze dell'app mobile
- **Open-sourced** nel 2015
- **Definizione**: linguaggio di query per API + runtime per eseguirle con dati esistenti
- Non è un database, non è un ORM — è un **contratto** tra client e server

### ⚠️ N+1 Problem — Bilanciamento critico

GraphQL non è privo di rischi. Il problema N+1 è il più comune:

```graphql
# Questa query potrebbe generare 1 + N query al DB
{
  users {          # 1 query: SELECT * FROM users
    posts {        # N query: SELECT * FROM posts WHERE user_id = ?
      title
    }
  }
}
```

**Soluzione**: DataLoader pattern (batching + caching per request).

```csharp
// HotChocolate DataLoader example
public class PostsByUserIdBatchDataLoader : BatchDataLoader<int, Post[]>
{
    // Raggruppa tutte le richieste in una singola query SQL
}
```

### 🗄️ Caching

REST beneficia del caching HTTP nativo (`GET` + `Cache-Control` + CDN). GraphQL usa `POST`, quindi:

| Strategia | Descrizione |
|-----------|-------------|
| **Persisted Queries** | Hash delle query → cacheable via CDN come GET |
| **Client Cache** | Apollo Client normalizza e deduplica in-memory |
| **Server Cache** | Response caching middleware (HotChocolate `UseQueryResult`) |
| **CDN Layer** | Automatic Persisted Queries (APQ) → CDN-friendly |

### 🔒 Security

Considerazioni di sicurezza per produzione:

| Rischio | Mitigazione |
|---------|-------------|
| Query troppo profonde | **Depth limiting** (max depth = 10-15) |
| Query troppo costose | **Cost analysis** (complessità stimata vs soglia) |
| Introspection in prod | **Disabilitare introspection** in produzione |
| Rate limiting | **Throttling** per IP/user/query complexity |
| Injection | Schema tipizzato previene SQL injection; validare input custom scalar |

### 📐 Schema Design Best Practices

- **Naming**: `camelCase` per campi, `PascalCase` per tipi
- **Nullability**: campi non-null di default, nullable solo se necessario
- **Pagination**: usare il pattern Relay Connection (`edges`, `nodes`, `pageInfo`)
- **Errori**: usare union types per errori domain-specific anziché eccezioni generiche
- **Deprecation**: `@deprecated(reason: "Use newField instead")` anziché rimuovere campi

### 🏗️ Architettura — Federation / Gateway

Per microservizi, considerare:

- **Schema Federation**: ogni servizio possiede una porzione dello schema
- **Gateway**: punto di ingresso unico che compone gli schemi (Apollo Gateway, Hot Chocolate Fusion, GraphQL Mesh)
- **Subgraph**: ogni microservizio espone il proprio subgraph

```
┌──────────┐
│  Client   │
└─────┬─────┘
      │
┌─────▼─────┐
│  Gateway   │  ← compone gli schemi
└──┬──┬──┬───┘
   │  │  │
┌──▼┐┌▼──┐┌▼──┐
│Svc1││Svc2││Svc3│  ← subgraph autonomi
└────┘└────┘└────┘
```

---

## Risorse Aggiuntive

| Risorsa | Link |
|---------|------|
| Specifica GraphQL | [spec.graphql.org](https://spec.graphql.org) |
| Learn GraphQL | [graphql.org/learn](https://graphql.org/learn) |
| Apollo Docs | [apollographql.com/docs](https://www.apollographql.com/docs) |
| Hot Chocolate (.NET) | [chillicream.com/docs/hotchocolate](https://chillicream.com/docs/hotchocolate) |
| GraphQL Code Generator | [graphql-code-generator.com](https://the-guild.dev/graphql/codegen) |
| DataLoader Pattern | [github.com/graphql/dataloader](https://github.com/graphql/dataloader) |
