namespace SqlDapperDemo.Api.Dtos;

public class CaptainViewDto
{
    public string Name { get; set; } = string.Empty;
    public string Rank { get; set; } = string.Empty;
    public string? HomePlanet { get; set; }
    public DateTime Born { get; set; }
    public DateTime? Died { get; set; }
    public string? FactionName { get; set; }
    public string? FactionColour { get; set; }
    public string? StarshipName { get; set; }
    public string? StarshipRegistration { get; set; }
    public DateTime? StarshipCommissioned { get; set; }
    public DateTime? StarshipDecommissioned { get; set; }
}
