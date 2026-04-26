# Temporal Filtering Management

This document illustrates the architecture adopted for implementing temporal filtering based on HTTP headers across the project's GraphQL APIs.

## Architecture: Global Query Filters

The business domain requires that historical entities (such as `PersonName`, `LegalPerson`, `NaturalPerson`, etc.) be filtered based on temporal coordinates passed via HTTP headers (`x-temporal-mode`, `x-temporal-range-start`, `x-temporal-range-end`).

The solution relies on EF Core's native **Global Query Filters** (`HasQueryFilter`). A filter is dynamically registered via reflection on all entities implementing the `ITemporalEntity` interface, eliminating the need to manually modify each configuration within the `DbContext`.

```csharp
// Inside PersonContext.OnModelCreating
modelBuilder.Entity<TEntity>().HasQueryFilter(e =>
    e.ValidStartDate <= TemporalContext.QueryMaxStartDate &&
    e.ValidEndDate > TemporalContext.QueryMinEndDate);
```

This approach allows EF Core to generate the SQL execution plan **only once**, cache it, and pass `@p0` and `@p1` as simple parameters at runtime, thereby maintaining maximum compilation performance.

---

## The "Mathematical Trick" for SQL Performance

A significant challenge posed by Global Query Filters was the logical complexity of the 4 temporal scenarios:
- **AsOf**: Find records valid at a specific instant.
- **AnyTimeIn**: Find records that overlap with a period (Start/End).
- **Throughout**: Find records that entirely cover a period (Start/End).
- **All**: No filter (return the entire history).
- **Default (No headers)**: Return only "Active" records today.

If we had implemented these conditions within a single Global Filter using multiple `OR` clauses dependent on a switch (`@Mode = 1 OR @Mode = 2...`), SQL Server would have suffered from **Parameter Sniffing**. This would have disabled the query optimizer's ability to utilize indexes on the dates, degrading the queries into slow *Table Scans*.

### Universal Simplification (No-ORs)

The adopted solution uses a mathematical trick that reduces ALL 4 logical cases down to a **single universal condition** (Bounding Box intersection):

```sql
WHERE ValidStartDate <= @QueryMaxStartDate AND ValidEndDate > @QueryMinEndDate
```

In `TemporalContext.cs`, we calculate the optimal boundaries (`QueryMaxStartDate` and `QueryMinEndDate`) in C# based on the requested mode. This guarantees an extremely simple and perfectly indexable statement for SQL Server.

#### The "Magic Date" (Active Records)

A key element of this implementation is the concept of the **"Magic Date"** for active records. In the database, a record that is currently active (not expired) has its `ValidEndDate` set to the maximum possible date value (`9999-12-31 23:59:59.9999999` in SQL Server).
When the user does not provide any temporal headers, the system defaults to querying "Active" records. To achieve this without adding an `IF` statement or an `OR` in the SQL, we pass `DateTime.MaxValue` as the upper bound, and `DateTime.MaxValue.AddTicks(-1)` as the lower bound. This forces the SQL condition to effectively become `ValidEndDate == DateTime.MaxValue`, ensuring only active records are fetched.

#### Case Mapping

| Mode | User Input | `QueryMaxStartDate` Calculation | `QueryMinEndDate` Calculation | Resulting SQL Condition (Simplified) |
|---|---|---|---|---|
| **Default (Active)** | N/A | `DateTime.MaxValue` | `DateTime.MaxValue.AddTicks(-1)` | `EndDate > MaxValue - 1` (i.e., `EndDate == Magic Date`) |
| **AsOf** | `X` | `X` | `X` | `StartDate <= X AND EndDate > X` |
| **AnyTimeIn** | `S`, `E` | `E.AddTicks(-1)` | `S` | `StartDate < E AND EndDate > S` |
| **Throughout** | `S`, `E` | `S` | `E.AddTicks(-1)` | `StartDate <= S AND EndDate >= E` |
| **All** | N/A | `DateTime.MaxValue` | `DateTime.MinValue` | `StartDate <= Max AND EndDate > Min` (Always true) |

### Benefits

1. **Zero C# Recompilations**: EF Core generates a single Service Provider and never flushes the internal cache.
2. **Best SQL Server Execution Plan**: Without any `OR` operators, the query natively exploits the multi-column B-Tree index on `(ValidStartDate, ValidEndDate)`. There is zero risk of Parameter Sniffing because the shape of the query never changes.
3. **Transparent Abstraction**: Developers writing GraphQL DataLoaders don't need to remember to add `.Where()` or `TagWith()` clauses. As long as the entity extends `ITemporalEntity`, it is filtered completely securely and automatically.
