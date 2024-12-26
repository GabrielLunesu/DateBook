using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.Models
{
    public class Date
    {
        [Key]
        public int DateId { get; set; }
        
        // Foreign keys
        public int UserId { get; set; }
        public int DateUserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
        
        [ForeignKey("DateUserId")]
        public virtual AppUser DateUser { get; set; }
        
        public string Location { get; set; }
        
        public DateTime DateTime { get; set; }
        
        public string Status { get; set; }
    }
} 