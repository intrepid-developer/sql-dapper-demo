namespace SqlDapperDemo.Api.Entities;

public class Captain
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Rank { get; set; } = string.Empty;
    public string? HomePlanet { get; set; }
    public DateTime Born { get; set; }
    public DateTime? Died { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}

