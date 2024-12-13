namespace DatingApp.DTOs
{
    public class CreateMatchDTO
    {
        public int UserId { get; set; }
        public int MatchedUserId { get; set; }
        public float MatchPercentage { get; set; }
        // Status will be set to default in controller
        // CreatedAt will be set in controller
    }
} 