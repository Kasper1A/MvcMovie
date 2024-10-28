namespace TodoApi.Models;
public class Movie
{
    public int Id { get; set; }
    public DateTime Time { get; set; }
    public string Title { get; set; } = "No title";
    public string? Description { get; set; }
}

