using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.Models
{
    public class QuizResponse
    {
        [Key]
        public int Id { get; set; }
        public int QuizId { get; set; }

        [ForeignKey("QuizId")]

        public virtual Quiz Quiz { get; set; }

        public int UserId {get; set;}
        
        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }

        
        
        // public string AgePreference { get; set; } 
        
        // public string RelationshipType { get; set; }
        
        // public int SportImportance { get; set; }
        
        // public int SocialLevel { get; set; }
        
        // public string WeekendActivity { get; set; }

        public bool UserResponse {get; set;}
        

        public DateTime CompletedAt { get; set; } = DateTime.Now;
    }
} 