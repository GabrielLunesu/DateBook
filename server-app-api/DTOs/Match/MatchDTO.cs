namespace DatingApp.DTOs
{
    public class MatchDTO
    {
        public int MatchId { get; set; }
        public int UserId { get; set; }
        public int MatchedUserId { get; set; }
        public float MatchPercentage { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        
        // Include basic user info for display
        public string MatchedUserName { get; set; }
        public string MatchedUserPhoto { get; set; }
    }
} 