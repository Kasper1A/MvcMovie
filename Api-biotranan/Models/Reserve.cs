namespace TodoApi.Models;

public class Reserve
{
    public int Id { get; set; }
    public int VisningId { get; set; }
    public Visning? Visning { get; set; }
}

