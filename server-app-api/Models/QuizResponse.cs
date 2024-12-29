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

    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual AppUser User { get; set; }


    [NotMapped]
    public List<bool> QuizAnswers {get; set;} = new();



    public bool UserResponse { get; set; }

    public DateTime CompletedAt { get; set; } = DateTime.Now;
}
} 