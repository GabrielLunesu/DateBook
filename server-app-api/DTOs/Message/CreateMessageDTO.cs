namespace DatingApp.DTOs
{
    public class CreateMessageDTO
    {
        public int ChatId { get; set; }
        public string Content { get; set; }
        // Timestamp will be set in controller
        // IsRead will be set to false by default
    }
} 