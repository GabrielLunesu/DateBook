using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.Models
{
    public class Chat
    {
        [Key]
        public int ChatId { get; set; }
        
        // Foreign keys
        public int UserId { get; set; }
        public int ChatUserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        
        [ForeignKey("ChatUserId")]
        public virtual User ChatUser { get; set; }
        
        public string Status { get; set; }
        
        public DateTime LastMessage { get; set; }
        
        // Navigation property
        public virtual ICollection<Message> Messages { get; set; }
    }
} 