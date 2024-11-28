using System.ComponentModel.DataAnnotations;

namespace DatingApp.Models
{
    public class UserType
    {
        [Key]
        public int TypeId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string[] Permissions { get; set; }
        
        // Navigation property
        public virtual ICollection<User> Users { get; set; }
    }
} 