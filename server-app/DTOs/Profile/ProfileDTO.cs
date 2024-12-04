namespace DatingApp.DTOs
{
    public class ProfileDTO
    {
        public int ProfileId { get; set; }
        public string Bio { get; set; }
        public string Gender { get; set; }
        public string[] Preferences { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public DateTime LastActive { get; set; }
    }
} 