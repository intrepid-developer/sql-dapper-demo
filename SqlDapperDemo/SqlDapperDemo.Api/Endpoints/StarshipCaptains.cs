using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SqlDapperDemo.Api.Entities;

namespace SqlDapperDemo.Api.Endpoints;

public static class StarshipCaptains
{
  public static void MapStarshipCaptainEndpoints(this WebApplication app)
  {
    app.MapGet("starship-captains", async ([FromServices] SqlConnection db) =>
    {
      var items = await db.QueryAsync<StarshipCaptain>("""
          SELECT Id, StarshipId, CaptainId, CreatedAt
          FROM dbo.StarshipCaptain
          ORDER BY Id DESC
          """);
      return Results.Ok(items);
    });

    app.MapGet("starship-captains/{id:int}", async (int id, [FromServices] SqlConnection db) =>
    {
      var item = await db.QuerySingleOrDefaultAsync<StarshipCaptain>("""
          SELECT Id, StarshipId, CaptainId, CreatedAt
          FROM dbo.StarshipCaptain
          WHERE Id = @id
          """, new { id });
      return item is null ? Results.NotFound() : Results.Ok(item);
    });

    app.MapPost("starship-captains", async ([FromBody] StarshipCaptain starshipCaptain, [FromServices] SqlConnection db) =>
    {
      var created = await db.QuerySingleAsync<StarshipCaptain>("""
          INSERT dbo.StarshipCaptain (StarshipId, CaptainId)
          OUTPUT INSERTED.Id, INSERTED.StarshipId, INSERTED.CaptainId, INSERTED.CreatedAt
          VALUES (@StarshipId, @CaptainId)
          """, starshipCaptain);
      return Results.Created($"/starship-captains/{created.Id}", created);
    });

    app.MapDelete("starship-captains/{id:int}", async (int id, [FromServices] SqlConnection db) =>
    {
      var affected = await db.ExecuteAsync("""
          DELETE FROM dbo.StarshipCaptain WHERE Id = @id
          """, new { id });
      return affected == 0 ? Results.NotFound() : Results.NoContent();
    });
  }
}
