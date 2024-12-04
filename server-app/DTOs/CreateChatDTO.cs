namespace DatingApp.DTOs
{
    public class CreateChatDTO
    {
        public int UserId { get; set; }
        public int ChatUserId { get; set; }
        public string Status { get; set; }
    }
} 