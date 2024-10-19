using MvcMovie.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MvcMovie.Services
{
    public class MovieService
    {
        private readonly HttpClient _httpClient;

        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Hämta en lista med filmer
        public async Task<List<Movie>> GetMoviesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("http://localhost:5296/api/movies");

                if (response.IsSuccessStatusCode)
                {
                    var movies = await response.Content.ReadFromJsonAsync<List<Movie>>();
                    return movies ?? new List<Movie>();
                }
                else
                {
                    Console.WriteLine($"API call failed with status code: {response.StatusCode}");
                    return new List<Movie>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching movies: {ex.Message}");
                return new List<Movie>();
            }
        }
        public async Task<List<Visning>> GetVisningsAsync()
        {
            try
            {
                // Hämta alla visningar från API:et
                var allShows = await _httpClient.GetFromJsonAsync<List<Visning>>("http://localhost:5296/api/Visningar");

                if (allShows == null)
                {
                    // Om inga visningar returneras från API:et, returnera en tom lista
                    return new List<Visning>();
                }

                // Returnera de hämtade visningarna
                return allShows;
            }
            catch (HttpRequestException ex)
            {
                // Logga eventuella fel under hämtningen av visningarna
                Console.WriteLine($"Error fetching visningar: {ex.Message}");
                return new List<Visning>(); // Returnera en tom lista vid fel
            }
        }


        // Hämta detaljer för en specifik film
        public async Task<Movie> GetMovieDetailsAsync(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5296/api/movies/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Movie>();
            }
            return null;
        }

        // Lägga till en ny film
        public async Task AddMovieAsync(Movie movie)
        {
            await _httpClient.PostAsJsonAsync("http://localhost:5296/api/movies", movie);
        }

        // Ta bort en film
        public async Task DeleteMovieAsync(int id)
        {
            await _httpClient.DeleteAsync($"http://localhost:5296/api/movies/{id}");
        }

        // Reservera platser för en film
        public async Task<bool> ReserveSeatsAsync(int movieId, int seats)
        {
            try
            {///
                var response = await _httpClient.PostAsJsonAsync($"http://localhost:5296/api/movies/{movieId}/reserve", seats);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"An error occurred while reserving seats: {ex.Message}");
                return false; // Logga eller hantera fel som behövs
            }
        }

        // Hämta tillgängliga biljetter för en specifik film
        public async Task<int> GetAvailableSeatsAsync(int visningId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"http://localhost:5296/api/Visningar/{visningId}/available-seats");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<int>();
                }
                return 0; // Returnera 0 om anropet misslyckas
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"An error occurred while fetching available seats: {ex.Message}");
                return 0; // Returnera 0 vid fel
            }
        }
    }
}
