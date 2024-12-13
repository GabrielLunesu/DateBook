using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        
        // Foreign key
        public int ChatId { get; set; }
        
        [ForeignKey("ChatId")]
        public virtual Chat Chat { get; set; }
        
        public string Content { get; set; }
        
        public DateTime Timestamp { get; set; }
        
        public bool IsRead { get; set; }
    }
} 