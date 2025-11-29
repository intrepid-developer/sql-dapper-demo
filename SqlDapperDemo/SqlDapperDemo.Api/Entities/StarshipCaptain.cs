namespace SqlDapperDemo.Api.Entities;

public class StarshipCaptain
{
    public int Id { get; set; }
    public int StarshipId { get; set; }
    public int CaptainId { get; set; }
    public DateTime CreatedAt { get; set; }
}

