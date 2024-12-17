using System.Text.Json;
using System.Text.Json.Nodes;

namespace ui.Helpers;

public static class JwtDecoder
{
    public static string GetUserIdFromToken(string token)
    {
        try
        {
            var payload = token.Split('.')[1];
            var paddedPayload = payload.PadRight(4 * ((payload.Length + 3) / 4), '=');
            var decodedBytes = Convert.FromBase64String(paddedPayload);
            var decodedJson = System.Text.Encoding.UTF8.GetString(decodedBytes);
            var jsonObject = JsonNode.Parse(decodedJson);
            
            // Get the "nameid" claim which typically contains the user ID
            return jsonObject["nameid"]?.GetValue<string>() 
                ?? throw new Exception("User ID not found in token");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error decoding JWT token: {ex.Message}");
        }
    }
} 