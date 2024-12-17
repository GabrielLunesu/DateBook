namespace DatingApp.DTOs
{
    public class CreateQuizDTO
    {
        public int UserId { get; set; }
        public string AgePreference { get; set; }
        public string RelationshipType { get; set; }
        public int SportImportance { get; set; }
        public int SocialLevel { get; set; }
        public string WeekendActivity { get; set; }
        // CompletedAt will be set in controller
    }
} 