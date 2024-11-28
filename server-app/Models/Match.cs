using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.Models
{
    public class Match
    {
        [Key]
        public int MatchId { get; set; }
        
        // Foreign keys
        public int UserId { get; set; }
        public int MatchedUserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        
        [ForeignKey("MatchedUserId")]
        public virtual User MatchedUser { get; set; }
        
        public float MatchPercentage { get; set; }
        
        public string Status { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
} 