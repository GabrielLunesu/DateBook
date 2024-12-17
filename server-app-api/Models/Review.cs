using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        
        // Foreign keys
        public int UserId { get; set; }
        public int ReviewedUserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
        
        [ForeignKey("ReviewedUserId")]
        public virtual AppUser ReviewedUser { get; set; }
        
        public int Rating { get; set; }
        
        public string Comment { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
} 