using System.Data;
using Dapper;
using SqlDapperDemo.Api.Entities;

namespace SqlDapperDemo.Api.Endpoints;

public static class Starships
{
  public static void MapStarshipEndpoints(this WebApplication app)
  {
    app.MapGet("starships", async (IDbConnection db) =>
    {
      var items = await db.QueryAsync<Starship>("""
          SELECT Id, Name, Registration, Commissioned, Decommissioned, ClassId, CreatedAt, LastUpdatedAt
          FROM dbo.Starship
          ORDER BY Name
          """);
      return Results.Ok(items);
    });

    app.MapGet("starships/{id:int}", async (int id, IDbConnection db) =>
    {
      var item = await db.QuerySingleOrDefaultAsync<Starship>("""
          SELECT Id, Name, Registration, Commissioned, Decommissioned, ClassId, CreatedAt, LastUpdatedAt
          FROM dbo.Starship
          WHERE Id = @id
          """, new { id });
      return item is null ? Results.NotFound() : Results.Ok(item);
    });

    app.MapPost("starships", async (Starship input, IDbConnection db) =>
    {
      var created = await db.QuerySingleAsync<Starship>("""
          INSERT dbo.Starship (Name, Registration, Commissioned, Decommissioned, ClassId)
          OUTPUT INSERTED.Id, INSERTED.Name, INSERTED.Registration, INSERTED.Commissioned, INSERTED.Decommissioned,
                 INSERTED.ClassId, INSERTED.CreatedAt, INSERTED.LastUpdatedAt
          VALUES (@Name, @Registration, @Commissioned, @Decommissioned, @ClassId)
          """, input);
      return Results.Created($"/starships/{created.Id}", created);
    });

    app.MapPut("starships/{id:int}", async (int id, Starship input, IDbConnection db) =>
    {
      var affected = await db.ExecuteAsync("""
          UPDATE dbo.Starship SET
            Name = @Name,
            Registration = @Registration,
            Commissioned = @Commissioned,
            Decommissioned = @Decommissioned,
            ClassId = @ClassId,
            LastUpdatedAt = SYSUTCDATETIME()
          WHERE Id = @Id
          """,
          new
          {
            Id = id,
            input.Name,
            input.Registration,
            input.Commissioned,
            input.Decommissioned,
            input.ClassId
          });
      return affected == 0 ? Results.NotFound() : Results.NoContent();
    });

    app.MapDelete("starships/{id:int}", async (int id, IDbConnection db) =>
    {
      var affected = await db.ExecuteAsync("""
          DELETE FROM dbo.Starship WHERE Id = @id
          """, new { id });
      return affected == 0 ? Results.NotFound() : Results.NoContent();
    });
  }
}

