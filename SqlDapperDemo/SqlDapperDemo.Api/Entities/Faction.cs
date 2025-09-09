namespace SqlDapperDemo.Api.Entities;

public class Faction
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Colour { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}

