namespace DatingApp.DTOs
{
    public class MessageDTO
    {
        public int MessageId { get; set; }
        public int ChatId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }
    }
} 