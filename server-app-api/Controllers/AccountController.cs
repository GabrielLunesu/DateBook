using dating_app_server.DTOs;
using dating_app_server.DTOs.Account;
using dating_app_server.Interfaces;
using dating_app_server.Models;
using dating_app_server.Services;
using DatingApp.DTOs;
using DatingApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dating_app_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(UserManager<AppUser> userManager, ITokenService tokenService,SignInManager<AppUser> signInManager, RoleManager<AppRole>  roleManager) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username))
            {
                return BadRequest("Username is taken");
            }
            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                Email = registerDto.Email.ToLower(),
                Name = registerDto.Name,
                Gender = registerDto.Gender,
                BirthDate = registerDto.BirthDate,
                Photos = registerDto.Photos,
                Location = registerDto.Location,
                IsActive = registerDto.IsActive,
                CreatedAt = registerDto.CreatedAt
            };
            user.UserTypeId = 1;  // Set default type to regular User
            var result= await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Member");
            }
            var response = new UserDTO();
            response.Token = await tokenService.CreateToken(user);
            response.Email = user.Email;
            response.Name = user.Name;
            response.IsActive = user.IsActive;
            response.CreatedAt = user.CreatedAt;
            response.UserId = user.Id;

            return response;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDto loginDto) {
        
            if(string.IsNullOrEmpty(loginDto.Username)|| string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest("UserName and Password are required field");
            }

            var user = await userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == loginDto.Username.ToUpper() ||
            u.NormalizedEmail == loginDto.Username.ToUpper()
            );
            if (user is null)
            {
                return Unauthorized("You are not authorized to login");
            }

            var result=await signInManager.CheckPasswordSignInAsync(user,loginDto.Password,lockoutOnFailure:false);
            if (!result.Succeeded) return BadRequest("Invalid credentials");
            if (result.Succeeded) {
                var response = new UserDTO();
                response.Token = await tokenService.CreateToken(user);
                response.Email = user.Email;
                response.Name = user.Name;
                response.IsActive = user.IsActive;
                response.CreatedAt = user.CreatedAt;
                response.UserId = user.Id;
                return Ok(response);

            }
            return BadRequest("Invalid Request");
        }





        [HttpGet("AddRoles")]
        public async Task<ActionResult<string>> AddRoles()
        {
            // Define the roles you want to add
            var roles = new List<string> { "Admin", "Member","Moderator" };
            var feedback = new List<string>();

            foreach (var role in roles)
            {
                // Check if the role exists
                if (await roleManager.RoleExistsAsync(role))
                {
                    feedback.Add($"Role '{role}' already exists.");
                }
                else
                {
                    // Add the role if it doesn't exist
                    var result = await roleManager.CreateAsync(new AppRole { Name = role });
                    if (result.Succeeded)
                    {
                        feedback.Add($"Role '{role}' successfully added.");
                    }
                    else
                    {
                        feedback.Add($"Failed to add role '{role}'. Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }

            return Ok(string.Join("\n", feedback));
        }



        private async Task<bool> UserExists(string username)
        {
            return await userManager.Users.AnyAsync(x => x.NormalizedUserName == username.ToUpper()); // Bob != bob
        }
    }


}
