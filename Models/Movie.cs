namespace MvcMovie.Models;
public class Movie
{
    public int Id { get; set; }
    public DateTime? Time { get; set; }
    public string? Title { get; set; }
    public int? Seat { get; set; }
    public string? Description { get; set; }
}