using TodoApi.Models;
namespace TodoApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    // public static List<Movie> _movies = new List<Movie>
    //     {
    //         new Movie { Id = 1, Title = "Inception", Description = "gammal mysig film"  },
    //         new Movie { Id = 2, Title = "The Matrix", Description = "ny omysig film" }
    //     };

    // GET: api/movies
    [HttpGet]
    public ActionResult<IEnumerable<Movie>> GetMovies()
    {
        using (var context = new TodoDbContext())
        {
            return Ok(context.Movies.ToList());
        }
    }

    // GET: api/movies
    [HttpGet("seed")]
    public ActionResult SeedMovies()
    {
        using (var context = new TodoDbContext())
        {
            var movie = new Movie { Id = 1, Title = "Inception", Description = "gammal mysig film" };
            var movie1 = new Movie { Id = 2, Title = "The Matrix", Description = "ny omysig film" };

            context.Movies.Add(movie);
            context.Movies.Add(movie1);
            context.SaveChanges();
        }
        return Ok();
    }

    // GET: api/movies/{id}
    [HttpGet("{id}")]
    public ActionResult<Movie> GetMovie(int id)
    {
        using (var context = new TodoDbContext())
        {
            var movie = context.Movies.Single(m => m.Id == id);
            return Ok(movie);
        }
    }

    // POST: api/movies
    [HttpPost]
    public ActionResult AddMovie([FromBody] Movie movie)
    {
        using (var context = new TodoDbContext())
        {
            context.Movies.Add(movie);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }
    }

    // DELETE: api/movies/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteMovie(int id)
    {
        using (var context = new TodoDbContext())
        {
            var movie = context.Movies.Single(m => m.Id == id);
            context.Movies.Remove(movie);
            context.SaveChanges();
        }
        return NoContent();
    }
}