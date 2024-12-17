// IQuizService.cs
using System.Net.Http.Json;
using System.Net.Http.Headers;
using ui.DTOs;
using ui.Helpers;

namespace ui.Services;

public interface IQuizService
{
    Task<bool> SubmitQuiz(QuizDTO quiz);
}

public class QuizService : IQuizService
{
    private readonly HttpClient _httpClient;

    public QuizService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> SubmitQuiz(QuizDTO quiz)
    {
        try
        {
            // Get the auth token
            var token = await TokenManager.GetAuthToken();
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("User not authenticated");
            }

            // Set the authorization header
            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsJsonAsync(Constants.QuizzesEndpoint, quiz);
            
            if (response.IsSuccessStatusCode)
                return true;

            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Quiz submission failed: {errorContent}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error submitting quiz: {ex.Message}", ex);
        }
    }
}