using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.Models
{
    public class Profile
    {
        [Key]
        public int ProfileId { get; set; }
        
        // Foreign key
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        
        public string Bio { get; set; }
        
        public string Gender { get; set; }
        
        public string[] Preferences { get; set; }
        
        public int MinAge { get; set; }
        
        public int MaxAge { get; set; }
        
        public DateTime LastActive { get; set; }
    }
} 