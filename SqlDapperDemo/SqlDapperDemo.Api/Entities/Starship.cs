namespace SqlDapperDemo.Api.Entities;

public class Starship
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Registration { get; set; } = string.Empty;
    public DateTime Commissioned { get; set; }
    public DateTime? Decommissioned { get; set; }
    public int ClassId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}

