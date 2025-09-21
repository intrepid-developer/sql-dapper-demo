# SQL + Dapper Demo (with .NET Aspire)

This repo backs a blog post about why understanding SQL still matters — even at a basic level — and how to pair that knowledge with Dapper for fast, explicit data access in .NET. The sample uses .NET Aspire to orchestrate a SQL Server container, deploy a database project, and run a minimal API that queries the database with Dapper.

**Quick start:** `dotnet run --project SqlDapperDemo/SqlDapperDemo.AppHost` (requires Docker and .NET 9 SDK)

**Why SQL Still Matters**
- Portability of knowledge: SQL is the common language across relational databases.
- Intent is explicit: You control joins, filters, ordering, and projections.
- Performance awareness: You can reason about query plans, indexes, and I/O.
- Easier debugging: You can run the exact statement in SSMS/ADS and iterate.
- Better modeling: Understanding tables, keys, and constraints leads to better schemas and simpler code.

You don’t need to be a DBA. Even basic SELECT/INSERT/UPDATE/DELETE, joins, and indexes will improve the quality and performance of your application code.

**How This Project Is Configured**
- `SqlDapperDemo.Api/` — Minimal API using Dapper; OpenAPI enabled in Development.
- `SqlDapperDemo.AppHost/` — .NET Aspire AppHost that:
  - starts a SQL Server container on `localhost:51000`,
  - creates a database named `sql-dapper-demo`,
  - deploys the SQL project (including post‑deploy seed data),
  - runs the API wired to the database via Aspire.
- `SqlDapperDemo.Database/` — SDK‑style SQL project (`.sqlproj`) with tables and a `PostDeploy` seed script (`Script.SeedData.sql`).
- `SqlDapperDemo.ServiceDefaults/` — Common telemetry, health checks, and service discovery.
- Solution: `SqlDapperDemo/SqlDapperDemo.sln` ties everything together.

Configuration flow:
- AppHost defines SQL (`AddSqlServer`) and the database resource (`AddDatabase("sql-dapper-demo")`).
- AppHost deploys the `.sqlproj` and waits for it to complete before starting the API.
- API calls `builder.AddSqlServerClient(connectionName: "sql-dapper-demo")` so a `SqlConnection` is injected where needed.

**Dapper: Lightweight ORM**

We use Dapper to map raw SQL results to C# objects without a heavy ORM.

- Connection: Injected into endpoints via DI as `SqlConnection` (provided by Aspire).
- Queries: Use `QueryAsync<T>`, `QuerySingleAsync<T>`, and `ExecuteAsync` with parameterized SQL.
- Mapping: Columns match properties on simple POCOs (e.g., `Starship`, `Captain`).

Example (simplified):

```csharp
// GET /starships
var items = await db.QueryAsync<Starship>(
    """
    SELECT Id, Name, Registration, Commissioned, Decommissioned, ClassId, CreatedAt, LastUpdatedAt
    FROM dbo.Starship
    ORDER BY Name
    """
);
```

Why choose Dapper here?
- Keeps SQL front-and-center so you learn it and reason about it.
- Great performance and low overhead.
- No hidden queries; what you write is what runs.

When a full ORM helps:
- Complex change tracking, LINQ-based composition, and migrations. This demo favors explicit SQL so the SQL itself is the focus.

**Run With .NET Aspire**

Prerequisites:
- .NET 9 SDK
- Docker Desktop running

Run everything (AppHost orchestrates SQL + DB deploy + API):
- `dotnet run --project SqlDapperDemo/SqlDapperDemo.AppHost`

What happens:
- SQL Server container starts on `localhost:51000` (persistent volume).
- Database `sql-dapper-demo` is created and the SQL project deploys.
- Seed data runs from `Script.SeedData.sql`.
- API starts with a connection string provided by Aspire.
- Console output shows service URLs; the API exposes OpenAPI in Development.

Stopping/cleanup:
- Stop the AppHost process to stop the API.
- The SQL container is persistent; if you need a clean slate, remove the container and volume via Docker.

**Using the API**

- OpenAPI is available in Development; the API exposes the OpenAPI document at `/openapi/v1.json`.
- Sample `.http` files are under `SqlDapperDemo.Api/Http/` (open in VS Code/JetBrains HTTP client):
  - `Captains.http`, `Starships.http`, `StarshipClasses.http`, `Factions.http`, `StarshipCaptains.http`.
- Typical resources and routes:
  - `GET /captains`, `POST /captains`, `PUT /captains/{id}`, `DELETE /captains/{id}`
  - `GET /starships`, `POST /starships`, etc.

Run API only (without AppHost):
- Ensure a SQL Server is reachable and set `ConnectionStrings__sql-dapper-demo` accordingly.
- `dotnet run --project SqlDapperDemo/SqlDapperDemo.Api`

Build database only:
- `dotnet build SqlDapperDemo/SqlDapperDemo.Database/SqlDapperDemo.Database.sqlproj`

**Data Model (High Level)**
- Starships, Captains, Factions, and Classes are represented as tables with standard keys/indexes.
- Seed data creates a small, queryable dataset for the demo.

**Key Takeaways**
- Knowing SQL — even the basics — makes your application code simpler, faster, and more predictable.
- Dapper keeps you close to the database while removing boilerplate mapping.
- Aspire streamlines local orchestration: database container, schema deployment, and API all in one command.
