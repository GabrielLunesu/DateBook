namespace DatingApp.DTOs
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int ReviewedUserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        // Include basic user info for display
        public string ReviewedUserName { get; set; }
        public string ReviewedUserPhoto { get; set; }
    }
} 