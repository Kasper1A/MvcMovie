namespace MvcMovie.Models;
public class Visning
{
    public int Id { get; set; }
    public Movie? Movie { get; set; }
    public DateTime Time { get; set; }
    public Salong? Salong { get; set; }
    public int MaxSeats { get; set; } = 25;
    public int ReservedSeats { get; set; } = 0;
}
