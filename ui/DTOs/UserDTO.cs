using System.Text.Json.Serialization;

namespace ui.DTOs;

public class UserDTO
{

    // we use this to map the json response to the dto
    // serializing is the process of converting json to C# objects
    // deserializing is the process of converting C# objects to json

    
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("birthDate")]
    public DateTime BirthDate { get; set; }

    [JsonPropertyName("photos")]
    public string[]? Photos { get; set; }

    [JsonPropertyName("location")]
    public string Location { get; set; }

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }
}