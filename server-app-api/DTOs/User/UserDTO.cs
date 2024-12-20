using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public  string Token { get; set; }=string.Empty;
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string[] Photos { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}