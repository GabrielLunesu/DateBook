namespace DatingApp.DTOs
{
    public class ChatDTO
    {
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public int ChatUserId { get; set; }
        public string Status { get; set; }
        public DateTime LastMessage { get; set; }
    }
}