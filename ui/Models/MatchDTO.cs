namespace ui.Models;

public class MatchDTO
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Location { get; set; }
    public double Compatibility { get; set; }
    public int MatchingAnswers { get; set; }
    public int TotalQuestions { get; set; }
    public List<string> Photos { get; set; }

    // Add a computed property for the full image URL
    public string MainPhotoUrl => 
        Photos?.FirstOrDefault() != null 
            ? $"{Constants.BaseApiUrl}/Images/{Photos.First()}"
            : "default_profile.png";
} 