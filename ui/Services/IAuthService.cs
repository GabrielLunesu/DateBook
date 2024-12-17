using System.Text;
using System.Text.Json;
using ui.DTOs;
using ui.Helpers;

namespace ui.Services
{
    public interface IAuthService
    {

        Task<UserDTO> Register(RegisterDTO registerDTO);
        Task<UserDTO> Login(LoginDTO loginDTO);
    }

    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        // we use the httpclient to send requests to the api
        // we inject it into the constructor (dependency injection)
        public AuthService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<UserDTO> Login(LoginDTO loginDTO)
        {
            var json = JsonSerializer.Serialize(loginDTO);
            // we create a string content object
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(Constants.LoginEndpoint, content);
            
            var responseBody = await response.Content.ReadAsStringAsync();

            if(response.IsSuccessStatusCode)
            {
                var userDTO = JsonSerializer.Deserialize<UserDTO>(responseBody);
                await TokenManager.SetAuthToken(userDTO.Token);
                TokenManager.SetUserProperties(userDTO);
                return userDTO;
            }
            throw new Exception($"Failed to login: {responseBody}");          
        }

        public async Task<UserDTO> Register(RegisterDTO registerDTO)
        {
            // we serialize the registerDTO to json
            var json = JsonSerializer.Serialize(registerDTO);
            // we create a string content object
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(Constants.RegisterEndpoint, content);
            
            var responseBody = await response.Content.ReadAsStringAsync();

            if(response.IsSuccessStatusCode)
            {
                var userDTO = JsonSerializer.Deserialize<UserDTO>(responseBody);
                await TokenManager.SetAuthToken(userDTO.Token);
                TokenManager.SetUserProperties(userDTO);
                return userDTO;
            }
            throw new Exception($"Failed to register: {responseBody}");            
        }
    }
}