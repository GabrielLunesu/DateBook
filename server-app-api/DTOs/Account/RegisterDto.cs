﻿using System.ComponentModel.DataAnnotations;

namespace dating_app_server.DTOs.Account
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
       
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; } = DateTime.Now.AddDays(-3333);

        public string[] Photos { get; set; }

        public string Location { get; set; } = "NetherLands";

        public bool IsActive { get; set; }=true;

        public DateTime CreatedAt { get; set; }=DateTime.Now;
        // public int UserTypeId { get; set; }
                // Removed UserTypeId since it will be set to 1 by default in the controller

    }
}
