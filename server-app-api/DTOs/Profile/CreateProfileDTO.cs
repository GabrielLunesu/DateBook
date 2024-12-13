namespace DatingApp.DTOs
{
    public class CreateProfileDTO
    {
        public int UserId { get; set; }
        public string Bio { get; set; }
        public string Gender { get; set; }
        public string[] Preferences { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        // LastActive will be set in controller
    }
} 