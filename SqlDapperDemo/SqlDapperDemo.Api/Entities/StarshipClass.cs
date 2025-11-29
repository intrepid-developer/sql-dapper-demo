namespace SqlDapperDemo.Api.Entities;

public class StarshipClass
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? FactionId { get; set; }
    public int? Length { get; set; }
    public int? Width { get; set; }
    public int? Depth { get; set; }
    public int? Decks { get; set; }
    public int? Height { get; set; }
    public int? MaxWarpSpeed { get; set; }
    public int? Crew { get; set; }
    public int? CargoCapacity { get; set; }
    public int? StarshipType { get; set; }
    public DateTime EnteredService { get; set; }
    public DateTime? ExitedService { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}

