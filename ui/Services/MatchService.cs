using System.Net.Http.Json;
using ui.Models;

namespace ui.Services;

public interface IMatchService
{
    Task<List<MatchDTO>> GetPotentialMatches(int userId);
}

public class MatchService : IMatchService
{
    private readonly HttpClient _httpClient;

    public MatchService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<MatchDTO>> GetPotentialMatches(int userId)
    {
        try
        {
            var url = string.Format(Constants.CalculateMatchingEndpoint, userId);
            Console.WriteLine($"Fetching matches from: {url}");
            
            var response = await _httpClient.GetAsync(url);
            Console.WriteLine($"Response status: {response.StatusCode}");
            
            if (response.IsSuccessStatusCode)
            {
                var matches = await response.Content.ReadFromJsonAsync<List<MatchDTO>>();
                Console.WriteLine($"Received {matches?.Count ?? 0} matches");
                return matches ?? new List<MatchDTO>();
            }
            
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error response: {error}");
            return new List<MatchDTO>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in GetPotentialMatches: {ex}");
            return new List<MatchDTO>();
        }
    }
} 