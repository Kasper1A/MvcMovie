using TodoApi.Models;
namespace TodoApi.Controllers;

using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class SalongController : ControllerBase
{


    [HttpPost]
    public ActionResult AddSalong([FromBody] Salong salong)
    {
        using (var context = new TodoDbContext())
        {
            context.Salongs.Add(salong);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetSalong), new { id = salong.Id }, salong);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<Salong> GetSalong(int id)
    {
        using (var context = new TodoDbContext())
        {
            var salong = context.Salongs.Single(s => s.Id == id);
            return Ok(salong);
        }
    }
}
