namespace DatingApp.DTOs
{
    public class UpdateProfileDTO
    {
        public string Bio { get; set; }
        public string Gender { get; set; }
        public string[] Preferences { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
    }
} 