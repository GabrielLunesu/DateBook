namespace DatingApp.DTOs
{
    public class CreateReviewDTO
    {
        public int UserId { get; set; }
        public int ReviewedUserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        // CreatedAt will be set in controller
    }
} 