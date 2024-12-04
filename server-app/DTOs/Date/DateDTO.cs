namespace DatingApp.DTOs
{
    public class DateDTO
    {
        public int DateId { get; set; }
        public int UserId { get; set; }
        public int DateUserId { get; set; }
        public string Location { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }

        // Include basic user info for display
        public string DateUserName { get; set; }
        public string DateUserPhoto { get; set; }
    }
} 