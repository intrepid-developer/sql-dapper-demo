using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using SqlDapperDemo.Api.Entities;

namespace SqlDapperDemo.Api.Endpoints;

public static class Factions
{
  public static void MapFactionEndpoints(this WebApplication app)
  {
    app.MapGet("factions", async ([FromServices] SqlConnection db) =>
    {
      var items = await db.QueryAsync<Faction>("""
          SELECT Id, Name, Colour, CreatedAt, LastUpdatedAt
          FROM dbo.Faction
          ORDER BY Name
          """);
      return Results.Ok(items);
    });

    app.MapGet("factions/{id:int}", async (int id, [FromServices] SqlConnection db) =>
    {
      var item = await db.QuerySingleOrDefaultAsync<Faction>("""
          SELECT Id, Name, Colour, CreatedAt, LastUpdatedAt
          FROM dbo.Faction
          WHERE Id = @id
          """, new { id });
      return item is null ? Results.NotFound() : Results.Ok(item);
    });

    app.MapPost("factions", async ([FromBody] Faction faction, [FromServices] SqlConnection db) =>
    {
      var created = await db.QuerySingleAsync<Faction>("""
          INSERT dbo.Faction (Name, Colour)
          OUTPUT INSERTED.Id, INSERTED.Name, INSERTED.Colour, INSERTED.CreatedAt, INSERTED.LastUpdatedAt
          VALUES (@Name, @Colour)
          """, faction);
      return Results.Created($"/factions/{created.Id}", created);
    });

    app.MapPut("factions/{id:int}", async (int id, [FromBody] Faction faction, [FromServices] SqlConnection db) =>
    {
      var affected = await db.ExecuteAsync("""
          UPDATE dbo.Faction
          SET Name = @Name,
              Colour = @Colour,
              LastUpdatedAt = GETUTCDATE()
          WHERE Id = @Id
          """, new { Id = id, faction.Name, faction.Colour });
      return affected == 0 ? Results.NotFound() : Results.NoContent();
    });

    app.MapDelete("factions/{id:int}", async (int id, [FromServices] SqlConnection db) =>
    {
      var affected = await db.ExecuteAsync("""
          DELETE FROM dbo.Faction WHERE Id = @id
          """, new { id });
      return affected == 0 ? Results.NotFound() : Results.NoContent();
    });
  }
}
