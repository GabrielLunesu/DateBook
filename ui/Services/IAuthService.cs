using System.Net.Http.Json;
using ui.DTOs;
using ui.Helpers;

namespace ui.Services;

public interface IAuthService
{
    Task<string> Login(LoginDTO loginDTO);
    Task<string> Register(RegisterDTO registerDTO);
}

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> Login(LoginDTO loginDTO)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(Constants.LoginEndpoint, loginDTO);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponseDTO>();
                if (result?.Token != null)
                {
                    var userId = JwtDecoder.GetUserIdFromToken(result.Token);
                    await TokenManager.SetAuthToken(result.Token);
                    await TokenManager.SetUserId(userId);
                    return result.Token;
                }
            }

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Login failed: {error}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Login error: {ex.Message}");
        }
    }

    public async Task<string> Register(RegisterDTO registerDTO)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(Constants.RegisterEndpoint, registerDTO);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponseDTO>();
                if (result?.Token != null)
                {
                    var userId = JwtDecoder.GetUserIdFromToken(result.Token);
                    await TokenManager.SetAuthToken(result.Token);
                    await TokenManager.SetUserId(userId);
                    return result.Token;
                }
            }

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Registration failed: {error}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Registration error: {ex.Message}");
        }
    }
}