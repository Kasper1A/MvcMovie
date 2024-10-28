using Microsoft.AspNetCore.Mvc;
using MvcMovie.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class VisningarController : Controller
    {
        private readonly MovieService _movieService;

        public VisningarController(MovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            // Hämta alla visningar
            var visningar = await _movieService.GetVisningsAsync();

            // Hämta tillgängliga platser för varje visning
            foreach (var visning in visningar)
            {
                visning.AvailableSeats = await _movieService.GetAvailableSeatsAsync(visning.Id);
            }

            // Skicka visningarna till vyn
            return View(visningar);
        }
    }
}
