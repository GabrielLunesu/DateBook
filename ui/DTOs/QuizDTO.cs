using System.Text.Json.Serialization;

namespace ui.DTOs;

public class QuizDTO
{
    [JsonPropertyName("quizId")]
    public int QuizId { get; set; }

    [JsonPropertyName("userId")]
    public int UserId { get; set; }

    [JsonPropertyName("agePreference")]
    public string AgePreference { get; set; }

    [JsonPropertyName("relationshipType")]
    public string RelationshipType { get; set; }

    [JsonPropertyName("sportImportance")]
    public int SportImportance { get; set; }

    [JsonPropertyName("socialLevel")]
    public int SocialLevel { get; set; }

    [JsonPropertyName("weekendActivity")]
    public string WeekendActivity { get; set; }

    [JsonPropertyName("completedAt")]
    public DateTime CompletedAt { get; set; }
}