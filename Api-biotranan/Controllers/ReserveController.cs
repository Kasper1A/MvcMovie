using TodoApi.Models;
namespace TodoApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class ReserveController : ControllerBase
{
    [HttpPost]
    public ActionResult ReserveVisning([FromBody] ReserveDto dto)
    {
        using (var context = new TodoDbContext())
        {
            var reserve = new Reserve();
            for (int i = 0; i < dto.Seats; i++)
            {
                context.Reservs.Add(reserve);

            }

            context.SaveChanges();
            return Ok(reserve);
        }
    }
}

public class ReserveDto
{
    public int VisningsId { get; set; }
    public int Seats { get; set; }
}