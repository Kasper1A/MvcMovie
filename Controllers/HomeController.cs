using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using MvcMovie.Services;

namespace MvcMovie.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MovieService _movieService;

    private readonly HttpClient _httpClient;
    public HomeController(ILogger<HomeController> logger, MovieService movieService, HttpClient httpClient)
    {
        _movieService = movieService;
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var visningar = await _httpClient.GetFromJsonAsync<List<Visning>>("http://localhost:5296/api/Movies");
            return View(visningar);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"An error occurred while fetching movies: {ex.Message}");
            return View(new List<Movie>());
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> Schema()
    {
        try
        {
            // Hämta visningar från API:et
            var visningar = await _httpClient.GetFromJsonAsync<List<Visning>>("http://localhost:5296/api/Visningar");

            // Kontrollera om vi har fått några visningar
            if (visningar == null || !visningar.Any())
            {
                ViewBag.Message = "Inga visningar hittades.";
                return View(new List<Visning>());
            }

            // Returnera vyn med listan av visningar
            return View(visningar);
        }
        catch (HttpRequestException ex)
        {
            // Logga fel och visa en felvy om något går fel
            _logger.LogError($"Ett fel uppstod vid hämtning av visningar: {ex.Message}");
            ViewBag.Message = "Ett fel uppstod vid hämtning av visningar.";
            return View(new List<Visning>());
        }
    }


    // GET: Action för att visa reserveringsformuläret
    [HttpGet]
    public IActionResult Reserve(int id)
    {
        var model = new ReservationDto { VisningsId = id };
        return View(model);
    }

    // Action för att visa bekräftelse efter reservation
    public IActionResult ReservationConfirmation(string code)
    {
        ViewBag.ConfirmationCode = code;
        return View();
    }

    // POST: Action för att hantera platsreservation
    [HttpPost]
    public async Task<IActionResult> Reserve(ReservationDto model)
    {
        if (ModelState.IsValid)
        {
            _logger.LogInformation($"Attempting to reserve {model.Seats} seats for movie ID {model.VisningsId}.");

            // Hämta tillgängliga platser
            var availableSeats = await _movieService.GetAvailableSeatsAsync(model.VisningsId);
            if (model.Seats <= availableSeats)
            {
                var success = await _movieService.ReserveSeatsAsync(model.VisningsId, model.Seats);
                if (success)
                {
                    // Generera en bekräftelsekod
                    var confirmationCode = GenerateConfirmationCode();

                    // Skicka bekräftelsekoden till vyn
                    TempData["ConfirmationCode"] = confirmationCode;

                    _logger.LogInformation($"Reservation successful. Confirmation code: {confirmationCode}");
                    return RedirectToAction("ReservationConfirmation", new { code = confirmationCode });
                }
                else
                {
                    _logger.LogError($"Reservation failed for movie ID {model.VisningsId}");
                    ModelState.AddModelError("", "Reservation failed. Please try again.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Not enough seats available for reservation.");
            }
        }
        else
        {
            _logger.LogWarning("Model state is invalid.");
        }

        return View(model); // Återgå till vyn om något går fel
    }
    // Generera en slumpmässig bekräftelsekod
    private string GenerateConfirmationCode()
    {
        Random random = new Random();
        return random.Next(100000, 999999).ToString(); // Genererar en 6-siffrig kod
    }

}
// Modell för reservation
public class ReservationDto
{
    public int VisningsId { get; set; }
    public int Seats { get; set; }
}