namespace ui.Models;

public class RegistrationModel
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public List<string>? Photos { get; set; } = null;
    public string Location { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
} 