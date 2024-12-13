using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DatingApp.Models;

namespace DatingApp.DTOs
{
    public class CreateUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public string Location { get; set; }

        public int UserTypeId { get; set; } = (int)UserType.UserTypeEnum.User;
    }
}

