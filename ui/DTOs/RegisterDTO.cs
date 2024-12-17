using System.ComponentModel.DataAnnotations;

namespace ui.DTOs
{
    public class RegisterDTO
    {
 
     
        public string Email { get; set; }
   
        public string Username { get; set; } = string.Empty;

       
        public string Password { get; set; } = string.Empty;
    
        public string Name { get; set; }

        public DateTime BirthDate { get; set; } = DateTime.Now.AddDays(-3333);

        public string[] Photos { get; set; } = Array.Empty<string>();

        public string? Location { get; set; } = "NetherLands";

        public bool? IsActive { get; set; }=true;

        public DateTime? CreatedAt { get; set; }=DateTime.Now;
       
    }
}
