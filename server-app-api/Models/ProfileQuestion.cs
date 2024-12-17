using DatingApp.Models;
using System.ComponentModel.DataAnnotations;

public class ProfileQuestion
{
    [Key]
    public int QuestionId { get; set; }
    
    public int UserId { get; set; }      // Question owner
    public AppUser User { get; set; }        
    public string Question { get; set; }  
    public bool CorrectAnswer { get; set; }  // True/False
    public bool IsActive { get; set; }    
    public bool? UserAnswer { get; set; }    // Null = not answered
    public int? AnsweredByUserId { get; set; }  // Who answered
    public AppUser AnsweredBy { get; set; }
    public DateTime? AnsweredAt { get; set; }
}