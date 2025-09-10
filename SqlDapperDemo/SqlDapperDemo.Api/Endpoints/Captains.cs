using System.Data;
using Dapper;
using SqlDapperDemo.Api.Entities;

namespace SqlDapperDemo.Api.Endpoints;

public static class Captains
{
  public static void MapCaptainEndpoints(this WebApplication app)
  {
    // List all captains
    app.MapGet("captains", async (IDbConnection db) =>
    {
      var items = await db.QueryAsync<Captain>("""
          SELECT Id, Name, Rank, HomePlanet, Born, Died, CreatedAt, LastUpdatedAt
          FROM dbo.Captain
          ORDER BY Name
          """);
      return Results.Ok(items);
    });

    // Get captain by id
    app.MapGet("captains/{id:int}", async (int id, IDbConnection db) =>
    {
      var item = await db.QuerySingleOrDefaultAsync<Captain>("""
          SELECT Id, Name, Rank, HomePlanet, Born, Died, CreatedAt, LastUpdatedAt
          FROM dbo.Captain
          WHERE Id = @id
          """, new { id });
      return item is null ? Results.NotFound() : Results.Ok(item);
    });

    // Create captain
    app.MapPost("captains", async (Captain input, IDbConnection db) =>
    {
      var created = await db.QuerySingleAsync<Captain>("""
          INSERT dbo.Captain (Name, Rank, HomePlanet, Born, Died)
          OUTPUT INSERTED.Id, INSERTED.Name, INSERTED.Rank, INSERTED.HomePlanet, INSERTED.Born, INSERTED.Died, INSERTED.CreatedAt, INSERTED.LastUpdatedAt
          VALUES (@Name, @Rank, @HomePlanet, @Born, @Died)
          """, input);
      return Results.Created($"/captains/{created.Id}", created);
    });

    // Update captain
    app.MapPut("captains/{id:int}", async (int id, Captain input, IDbConnection db) =>
    {
      var affected = await db.ExecuteAsync("""
          UPDATE dbo.Captain
          SET Name = @Name,
              Rank = @Rank,
              HomePlanet = @HomePlanet,
              Born = @Born,
              Died = @Died,
              LastUpdatedAt = GETUTCDATE()
          WHERE Id = @Id
          """, new
      {
        Id = id,
        input.Name,
        input.Rank,
        input.HomePlanet,
        input.Born,
        input.Died
      });
      return affected == 0 ? Results.NotFound() : Results.NoContent();
    });

    // Delete captain
    app.MapDelete("captains/{id:int}", async (int id, IDbConnection db) =>
    {
      var affected = await db.ExecuteAsync("""
          DELETE FROM dbo.Captain WHERE Id = @id
          """, new { id });
      return affected == 0 ? Results.NotFound() : Results.NoContent();
    });
  }
}
