using dating_app_server.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DatingApp.Models.Enums;
namespace DatingApp.Models
{
    public class AppUser:IdentityUser<int>
    {
        //[Key]
        //public int UserId { get; set; }
        
        //[Required]
        //[EmailAddress]
        //public string Email { get; set; }
        
        //[Required]
        //public string Password { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public string[] Photos { get; set; }

        public Gender Gender { get; set; }
        public string Location { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        // Foreign key
        public int UserTypeId { get; set; }
        
        // Navigation properties
        [ForeignKey("UserTypeId")]
        public virtual UserType UserType { get; set; }
        
        public virtual Profile Profile { get; set; }
        
        public virtual Quiz Quiz { get; set; }
        
        public virtual ICollection<Match> Matches { get; set; }
        
        public virtual ICollection<Date> Dates { get; set; }
        
        public virtual ICollection<Review> Reviews { get; set; }
        
        public virtual ICollection<Chat> Chats { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; } = [];
        public List<ProfileQuestion> Questions { get; set; }            // Questions they created
        public List<ProfileQuestion> AnsweredQuestions { get; set; }    // Questions they answered
    }
} 