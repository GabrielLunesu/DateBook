namespace DatingApp.DTOs
{
    public class UpdateUserDTO
    {
        public string Name { get; set; }
        public string[] Photos { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
    }
} 