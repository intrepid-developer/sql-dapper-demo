# Repository Guidelines

## Project Structure & Module Organization
- `SqlDapperDemo.Api/` — Minimal API with OpenAPI in Development; uses shared service defaults.
- `SqlDapperDemo.AppHost/` — .NET Aspire AppHost orchestrating the API and a SQL Server container; deploys the database project.
- `SqlDapperDemo.Database/` — SQL Server database project (`.sqlproj`), schema under `Tables/`, seed data in `Script.SeedData.sql`.
- `SqlDapperDemo.ServiceDefaults/` — Common telemetry, health checks, service discovery, and HTTP client resilience.
- `SqlDapperDemo.sln` — Solution entry point for building all projects.

## Build, Test, and Development Commands
- Restore/build: `dotnet restore && dotnet build SqlDapperDemo.sln -c Debug`
- Run everything (requires Docker): `dotnet run --project SqlDapperDemo.AppHost`
  - AppHost will start SQL Server, deploy the `.sqlproj`, and run the API.
- Run API only: `dotnet run --project SqlDapperDemo.Api`
  - OpenAPI is exposed at `/swagger` in Development.
- Build database only: `dotnet build SqlDapperDemo.Database/SqlDapperDemo.Database.sqlproj`

## Coding Style & Naming Conventions
- C#: 4‑space indentation; `PascalCase` for types/methods, `camelCase` for locals/parameters, interface prefix `I`, async suffix `Async`.
- Endpoints: prefer lower‑case, dash‑separated routes (e.g., `/starships`), minimal API patterns.
- SQL: tables `PascalCase`; columns `PascalCase`; FKs `FK_<Table>_<Ref>`; indexes `IX_<Table>_<Columns>`; keep DDL in `Tables/`, seed in `Script.SeedData.sql`.
- Formatting: `dotnet format` before pushing (if installed). Keep files small and focused.

## Testing Guidelines
- There are currently no tests. Prefer xUnit.
- Create test projects named `<ProjectName>.Tests` (e.g., `SqlDapperDemo.Api.Tests`).
- Test names: `MethodOrRoute_ShouldExpectedBehavior`.
- Run all tests: `dotnet test` at the repo root.

## Commit & Pull Request Guidelines
- Use Conventional Commits (`feat:`, `fix:`, `chore:`, `docs:`) with optional scope (`api`, `database`, `apphost`).
- PRs include: concise description, linked issues, local run steps (`dotnet run --project SqlDapperDemo.AppHost`), and relevant screenshots or `curl`/`.http` examples.
- For DB changes: update DDL in `Tables/` and adjust `Script.SeedData.sql` when needed; avoid destructive changes without clear migration notes.

## Security & Configuration Tips
- Requires Docker for local SQL container. Ensure `ASPNETCORE_ENVIRONMENT=Development` for local work.
- Do not commit secrets. Prefer environment variables or user‑secrets. Connection strings flow from AppHost; can be overridden via `ConnectionStrings__*` environment variables.
