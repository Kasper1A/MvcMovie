namespace MvcMovie.Models;

public class Visning
{
    public int Id { get; set; }
    public Movie? Movie { get; set; }
    public DateTime Time { get; set; }
    public int MaxSeats { get; set; }
    public Salong? Salong { get; set; }
    public int ReservedSeats { get; set; }
    // Ny egenskap för tillgängliga platser
    public int AvailableSeats { get; set; }
}
