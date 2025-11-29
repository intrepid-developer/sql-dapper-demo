using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SqlDapperDemo.Api.Entities;

namespace SqlDapperDemo.Api.Endpoints;

public static class StarshipClasses
{
  public static void MapStarshipClassEndpoints(this WebApplication app)
  {
    app.MapGet("starship-classes", async ([FromServices] SqlConnection db) =>
    {
      var items = await db.QueryAsync<StarshipClass>("""
          SELECT Id, Name, Description, FactionId, Length, Width, Depth, Decks, Height,
                 MaxWarpSpeed, Crew, CargoCapacity, StarshipType, EnteredService, ExitedService,
                 Active, CreatedAt, LastUpdatedAt
          FROM dbo.StarshipClass
          ORDER BY Name
          """);
      return Results.Ok(items);
    });

    app.MapGet("starship-classes/{id:int}", async (int id, [FromServices] SqlConnection db) =>
    {
      var item = await db.QuerySingleOrDefaultAsync<StarshipClass>("""
          SELECT Id, Name, Description, FactionId, Length, Width, Depth, Decks, Height,
                 MaxWarpSpeed, Crew, CargoCapacity, StarshipType, EnteredService, ExitedService,
                 Active, CreatedAt, LastUpdatedAt
          FROM dbo.StarshipClass
          WHERE Id = @id
          """, new { id });
      return item is null ? Results.NotFound() : Results.Ok(item);
    });

    app.MapPost("starship-classes", async ([FromBody] StarshipClass starshipClass, [FromServices] SqlConnection db) =>
    {
      var created = await db.QuerySingleAsync<StarshipClass>("""
          INSERT dbo.StarshipClass (
            Name, Description, FactionId, Length, Width, Depth, Decks, Height,
            MaxWarpSpeed, Crew, CargoCapacity, StarshipType, EnteredService, ExitedService, Active
          )
          OUTPUT INSERTED.Id, INSERTED.Name, INSERTED.Description, INSERTED.FactionId, INSERTED.Length, INSERTED.Width, INSERTED.Depth,
                 INSERTED.Decks, INSERTED.Height, INSERTED.MaxWarpSpeed, INSERTED.Crew, INSERTED.CargoCapacity, INSERTED.StarshipType,
                 INSERTED.EnteredService, INSERTED.ExitedService, INSERTED.Active, INSERTED.CreatedAt, INSERTED.LastUpdatedAt
          VALUES (
            @Name, @Description, @FactionId, @Length, @Width, @Depth, @Decks, @Height,
            @MaxWarpSpeed, @Crew, @CargoCapacity, @StarshipType, @EnteredService, @ExitedService, @Active
          )
          """, starshipClass);
      return Results.Created($"/starship-classes/{created.Id}", created);
    });

    app.MapPut("starship-classes/{id:int}", async (int id, [FromBody] StarshipClass starshipClass, [FromServices] SqlConnection db) =>
    {
      var affected = await db.ExecuteAsync("""
          UPDATE dbo.StarshipClass SET
            Name = @Name,
            Description = @Description,
            FactionId = @FactionId,
            Length = @Length,
            Width = @Width,
            Depth = @Depth,
            Decks = @Decks,
            Height = @Height,
            MaxWarpSpeed = @MaxWarpSpeed,
            Crew = @Crew,
            CargoCapacity = @CargoCapacity,
            StarshipType = @StarshipType,
            EnteredService = @EnteredService,
            ExitedService = @ExitedService,
            Active = @Active,
            LastUpdatedAt = GETUTCDATE()
          WHERE Id = @Id
          """,
          new
          {
            Id = id,
            starshipClass.Name,
            starshipClass.Description,
            starshipClass.FactionId,
            starshipClass.Length,
            starshipClass.Width,
            starshipClass.Depth,
            starshipClass.Decks,
            starshipClass.Height,
            starshipClass.MaxWarpSpeed,
            starshipClass.Crew,
            starshipClass.CargoCapacity,
            starshipClass.StarshipType,
            starshipClass.EnteredService,
            starshipClass.ExitedService,
            starshipClass.Active
          });
      return affected == 0 ? Results.NotFound() : Results.NoContent();
    });

    app.MapDelete("starship-classes/{id:int}", async (int id, [FromServices] SqlConnection db) =>
    {
      var affected = await db.ExecuteAsync("""
          DELETE FROM dbo.StarshipClass WHERE Id = @id
          """, new { id });
      return affected == 0 ? Results.NotFound() : Results.NoContent();
    });
  }
}
