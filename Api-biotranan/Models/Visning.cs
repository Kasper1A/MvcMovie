using System.Text.Json.Serialization;

namespace TodoApi.Models;
public class Visning
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public Movie? Movie { get; set; }
    public DateTime Time { get; set; }
    public int ReservedSeats { get; set; } = 0;
    public int SalongId { get; set; }
    public Salong? Salong { get; set; }

}

// public class Visningar
// {
//     public string Title { get; set; }
//     public DateTime Time { get; set; }
// }