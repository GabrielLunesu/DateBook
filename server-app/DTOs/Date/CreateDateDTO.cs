namespace DatingApp.DTOs
{
    public class CreateDateDTO
    {
        public int UserId { get; set; }
        public int DateUserId { get; set; }
        public string Location { get; set; }
        public DateTime DateTime { get; set; }
        // Status will be set to default in controller
    }
} 