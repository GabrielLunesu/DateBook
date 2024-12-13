using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.Models
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }
        
        // Foreign key
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
        
        public string AgePreference { get; set; } // Changed from range type to string
        
        public string RelationshipType { get; set; }
        
        public int SportImportance { get; set; }
        
        public int SocialLevel { get; set; }
        
        public string WeekendActivity { get; set; }
        
        public DateTime CompletedAt { get; set; }
    }
} 