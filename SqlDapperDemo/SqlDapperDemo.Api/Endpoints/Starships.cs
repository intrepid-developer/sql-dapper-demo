using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using SqlDapperDemo.Api.Entities;

namespace SqlDapperDemo.Api.Endpoints;

public static class Starships
{
  public static void MapStarshipEndpoints(this WebApplication app)
  {
    app.MapGet("starships", async ([FromServices] SqlConnection db) =>
    {
      var items = await db.QueryAsync<Starship>("""
          SELECT Id, Name, Registration, Commissioned, Decommissioned, ClassId, CreatedAt, LastUpdatedAt
          FROM dbo.Starship
          ORDER BY Name
          """);
      return Results.Ok(items);
    });

    app.MapGet("starships/{id:int}", async (int id, [FromServices] SqlConnection db) =>
    {
      var item = await db.QuerySingleOrDefaultAsync<Starship>("""
          SELECT Id, Name, Registration, Commissioned, Decommissioned, ClassId, CreatedAt, LastUpdatedAt
          FROM dbo.Starship
          WHERE Id = @id
          """, new { id });
      return item is null ? Results.NotFound() : Results.Ok(item);
    });

    app.MapPost("starships", async ([FromBody] Starship starship, [FromServices] SqlConnection db) =>
    {
      var created = await db.QuerySingleAsync<Starship>("""
          INSERT dbo.Starship (Name, Registration, Commissioned, Decommissioned, ClassId)
          OUTPUT INSERTED.Id, INSERTED.Name, INSERTED.Registration, INSERTED.Commissioned, INSERTED.Decommissioned,
                 INSERTED.ClassId, INSERTED.CreatedAt, INSERTED.LastUpdatedAt
          VALUES (@Name, @Registration, @Commissioned, @Decommissioned, @ClassId)
          """, starship);
      return Results.Created($"/starships/{created.Id}", created);
    });

    app.MapPut("starships/{id:int}", async (int id, [FromBody] Starship starship, [FromServices] SqlConnection db) =>
    {
      var affected = await db.ExecuteAsync("""
          UPDATE dbo.Starship SET
            Name = @Name,
            Registration = @Registration,
            Commissioned = @Commissioned,
            Decommissioned = @Decommissioned,
            ClassId = @ClassId,
            LastUpdatedAt = GETUTCDATE()
          WHERE Id = @Id
          """,
          new
          {
            Id = id,
            starship.Name,
            starship.Registration,
            starship.Commissioned,
            starship.Decommissioned,
            starship.ClassId
          });
      return affected == 0 ? Results.NotFound() : Results.NoContent();
    });

    app.MapDelete("starships/{id:int}", async (int id, [FromServices] SqlConnection db) =>
    {
      var affected = await db.ExecuteAsync("""
          DELETE FROM dbo.Starship WHERE Id = @id
          """, new { id });
      return affected == 0 ? Results.NotFound() : Results.NoContent();
    });
  }
}
