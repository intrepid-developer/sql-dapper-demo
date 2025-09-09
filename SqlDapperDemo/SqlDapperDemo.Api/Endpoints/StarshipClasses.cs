using System.Data;
using Dapper;
using SqlDapperDemo.Api.Entities;

namespace SqlDapperDemo.Api.Endpoints;

public static class StarshipClasses
{
  public static void MapStarshipClassEndpoints(this WebApplication app)
  {
    app.MapGet("starship-classes", async (IDbConnection db) =>
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

    app.MapGet("starship-classes/{id:int}", async (int id, IDbConnection db) =>
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

    app.MapPost("starship-classes", async (StarshipClass input, IDbConnection db) =>
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
          """, input);
      return Results.Created($"/starship-classes/{created.Id}", created);
    });

    app.MapPut("starship-classes/{id:int}", async (int id, StarshipClass input, IDbConnection db) =>
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
            LastUpdatedAt = SYSUTCDATETIME()
          WHERE Id = @Id
          """,
          new
          {
            Id = id,
            input.Name,
            input.Description,
            input.FactionId,
            input.Length,
            input.Width,
            input.Depth,
            input.Decks,
            input.Height,
            input.MaxWarpSpeed,
            input.Crew,
            input.CargoCapacity,
            input.StarshipType,
            input.EnteredService,
            input.ExitedService,
            input.Active
          });
      return affected == 0 ? Results.NotFound() : Results.NoContent();
    });

    app.MapDelete("starship-classes/{id:int}", async (int id, IDbConnection db) =>
    {
      var affected = await db.ExecuteAsync("""
          DELETE FROM dbo.StarshipClass WHERE Id = @id
          """, new { id });
      return affected == 0 ? Results.NotFound() : Results.NoContent();
    });
  }
}

