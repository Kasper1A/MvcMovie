using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi;
using TodoApi.Models;

[Route("api/[controller]")]
[ApiController]
public class VisningarController : ControllerBase
{
    private static List<Visning> _visning = new List<Visning>();

    // GET: api/Visningar
    [HttpGet]
    public ActionResult<IEnumerable<Visning>> GetVisningar()
    {
        List<Visning> visningar;
        using (var context = new TodoDbContext())
        {
            // Hämta alla visningar och inkludera relaterade filmer
            visningar = context.Visnings
                .Include(v => v.Salong)
                .Include(v => v.Movie)
                .ToList();
        }
        return Ok(visningar);
    }


    // POST: api/Visningar
    [HttpPost]
    public ActionResult<Visning> AddVisning([FromBody] Visning visningData)
    {
        using (var context = new TodoDbContext())
        {
            // Validera att visningData inte är null och att både titel och tid är angivna
            if (visningData == null || visningData.Time == default)
            {
                return BadRequest("Invalid visning data. Please provide both a title and a date.");
            }

            // Skapa en ny visning med den angivna titeln och tiden
            var newVisning = new Visning
            {
                Id = _visning.Any() ? _visning.Max(v => v.Id) + 1 : 1,
                Time = visningData.Time
            };

            context.Visnings.Add(visningData);
            context.SaveChanges();
            // Lägg till den nya visningen i listan
            _visning.Add(newVisning);

            // Returnera den nya visningen med CreatedAtAction för att visa var den skapades
            return CreatedAtAction(nameof(GetVisningar), new { id = newVisning.Id }, newVisning);
        }
    }
    // GET: api/Visningar
    [HttpGet("{id}/available-seats")]
    public ActionResult<int> GetAvailableSeats(int id)
    {
        using (var context = new TodoDbContext())
        {


            var visning = context.Visnings.Include(v => v.Salong).Single(v => v.Id == id);

            return Ok(visning.Salong!.MaxSeats - visning.ReservedSeats);
        }
    }
}