using MvcMovie.Models;
using MvcMovie.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieService _movieService;
        private readonly HttpClient _httpClient;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(MovieService movieService, HttpClient httpClient, ILogger<MoviesController> logger)
        {
            _movieService = movieService;
            _httpClient = httpClient;
            _logger = logger;
        }

        // Action för att visa alla filmer
        public async Task<IActionResult> Index()
        {
            try
            {
                var movies = await _httpClient.GetFromJsonAsync<List<Movie>>("http://localhost:5296/api/Movies");
                return View(movies);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occurred while fetching movies: {ex.Message}");
                return View(new List<Movie>());
            }
        }

        // Action för att visa detaljer om en specifik film
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieDetailsAsync(id);
            if (movie == null)
            {
                _logger.LogWarning($"Movie with ID {id} not found.");
                return NotFound();
            }

            var availableSeats = await _movieService.GetAvailableSeatsAsync(id);
            ViewBag.AvailableSeats = availableSeats;

            return View(movie);
        }


        // Action för att lägga till en film (GET)
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        // Action för att lägga till en film (POST)
        [HttpPost]
        public async Task<IActionResult> Add(Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _movieService.AddMovieAsync(movie);
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // Action för att ta bort en film (POST)
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _movieService.DeleteMovieAsync(id);
            return RedirectToAction("Index");
        }

        // Standard privacy-sida
        public IActionResult Privacy()
        {
            return View();
        }

        // Action för felhantering
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

}
