using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.Models
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }
        
        public string Question {get; set;}

        public bool Status {get; set;}

        // Navigation property
        public virtual ICollection<QuizResponse> QuizResponses { get; set; } = new List<QuizResponse>();
    }
} 