using MvcMovie.Models;

namespace MvcMovie;
public class Reserve
{
    public int Id { get; set; }
    public int VisningsId { get; set; }
    public Visning? Visning { get; set; }
}
