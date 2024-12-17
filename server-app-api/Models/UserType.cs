using System.ComponentModel.DataAnnotations;

namespace DatingApp.Models
{
    public class UserType
    {
        public enum UserTypeEnum
        {
            User = 1,
            Moderator = 2,
            Admin = 3
        }

        [Key]
        public int TypeId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        // Navigation property
        public virtual ICollection<AppUser> Users { get; set; }

        public static UserTypeEnum ToEnum(int typeId)
        {
            return (UserTypeEnum)typeId;
        }
    }
} 