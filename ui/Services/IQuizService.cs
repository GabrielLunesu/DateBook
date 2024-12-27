// IQuizService.cs
using System.Net.Http.Json;
using System.Net.Http.Headers;
using ui.DTOs;
using ui.Helpers;

namespace ui.Services;

public interface IQuizService
{
    Task<List<QuizDTO>> GetAllQuestionsAsync();
    Task<bool> SubmitQuizResponseAsync(QuizResponseDTO response);
    Task<List<QuizResponseDTO>> GetUserResponses();
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

            var response = await _httpClient.PostAsJsonAsync(Constants.QuizEndpoint, quiz);
            
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

    public async Task<List<QuizDTO>> GetAllQuestionsAsync()
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

            var response = await _httpClient.GetAsync(Constants.QuizEndpoint);
            
            if (response.IsSuccessStatusCode)
            {
                var quizzes = await response.Content.ReadFromJsonAsync<List<QuizDTO>>();
                return quizzes;
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error getting quizzes: {errorContent}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting quizzes: {ex.Message}", ex);
        }
    }

    public async Task<bool> SubmitQuizResponseAsync(QuizResponseDTO response)
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

            // Check if user has already answered this question
            var existingResponses = await GetUserResponses();
            if (existingResponses.Any(r => r.QuizId == response.QuizId))
            {
                throw new Exception("You have already answered this question");
            }

            // Submit new response
            var responseContent = await _httpClient.PostAsJsonAsync(
                Constants.QuizResponseEndpoint, 
                response);
            
            if (responseContent.IsSuccessStatusCode)
                return true;

            var errorContent = await responseContent.Content.ReadAsStringAsync();
            throw new Exception($"Quiz response submission failed: {errorContent}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error submitting quiz response: {ex.Message}", ex);
        }
    }

    public async Task<List<QuizResponseDTO>> GetUserResponses()
    {
        try
        {
            var token = await TokenManager.GetAuthToken();
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("User not authenticated");
            }

            // Get the current user ID
            var userId = await TokenManager.GetUserId();
            if (!userId.HasValue)
            {
                throw new Exception("User ID not found");
            }

            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", token);

            // Use string.Format to insert the userId into the endpoint URL
            var endpoint = string.Format(Constants.UserResponsesEndpoint, userId.Value);
            var response = await _httpClient.GetAsync(endpoint);
            
            if (response.IsSuccessStatusCode)
            {
                var responses = await response.Content.ReadFromJsonAsync<List<QuizResponseDTO>>();
                return responses ?? new List<QuizResponseDTO>();
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error getting user responses: {errorContent}");
        }
        catch (Exception ex)
        {
            // For debugging, log the error but return empty list to allow quiz to continue
            System.Diagnostics.Debug.WriteLine($"Error getting user responses: {ex.Message}");
            return new List<QuizResponseDTO>();
        }
    }
}