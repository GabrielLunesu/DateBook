using System.Net.Http.Json;
using ui.DTOs;
using ui.Helpers;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace ui.Services;

public interface IAuthService
{
    Task<string> Login(LoginDTO loginDTO);
    Task<string> Register(RegisterDTO registerDTO);
    Task<int> GetCurrentUserId();
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
                    await TokenManager.SetUserId(result.UserId);
                    return result.Token;
                }
            }

            var error = await response.Content.ReadAsStringAsync();
            // throw new Exception($"Login failed: {error}");
            Shell.Current.GoToAsync("//LoginErrorPage");
            return "error login";
            
        }
        catch (Exception ex)
        {
            await Shell.Current.GoToAsync("//LoginErrorPage");
            return  "error login";
            // throw new Exception($"Login error: {ex.Message}");
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
                    // var userId = JwtDecoder.GetUserIdFromToken(result.Token);
                    
                    await TokenManager.SetAuthToken(result.Token);
                    await TokenManager.SetUserId(result.UserId);
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

    public async Task<int> GetCurrentUserId()
    {
        var token = await TokenManager.GetAuthToken();
        if (string.IsNullOrEmpty(token))
            throw new Exception("User not authenticated");

        // Decode the JWT token to get the user ID
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        
        var userIdClaim = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "nameid");
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            throw new Exception("Could not determine user ID");

        return userId;
    }
}