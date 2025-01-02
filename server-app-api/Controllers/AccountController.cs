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
using DatingApp.Models.Enums;
using DatingApp.Data;

namespace dating_app_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(UserManager<AppUser> userManager, ITokenService tokenService,SignInManager<AppUser> signInManager, RoleManager<AppRole>  roleManager, ApplicationDbContext _context) : ControllerBase
    {

        //[HttpPost("register")]
        //public async Task<ActionResult<UserDTO>> Register(RegisterDto registerDto)
        //{
        //    if (await UserExists(registerDto.Username))
        //    {
        //        return BadRequest("Username is taken");
        //    }
        //    var user = new AppUser
        //    {
        //        UserName = registerDto.Username.ToLower(),
        //        Email = registerDto.Email.ToLower(),
        //        Name = registerDto.Name,
        //        Gender = registerDto.Gender,
        //        BirthDate = registerDto.BirthDate,
        //        Photos = registerDto.Photos,
        //        Location = registerDto.Location,
        //        IsActive = registerDto.IsActive,
        //        CreatedAt = registerDto.CreatedAt
        //    };
        //    user.UserTypeId = 1;  // Set default type to regular User
        //    var result= await userManager.CreateAsync(user, registerDto.Password);
        //    if (!result.Succeeded) return BadRequest(result.Errors);
        //    if (result.Succeeded)
        //    {
        //        await userManager.AddToRoleAsync(user, "Member");
        //    }
        //    var response = new UserDTO();
        //    response.Token = await tokenService.CreateToken(user);
        //    response.Email = user.Email;
        //    response.Name = user.Name;
        //    response.GenderName = user.Gender == Gender.Male ? "Male" : "Female";
        //    response.IsActive = user.IsActive;
        //    response.CreatedAt = user.CreatedAt;
        //    response.UserId = user.Id;

        //    return response;
        //}
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register( RegisterDto registerDto)
        {
            // Check if the username is already taken
            if (await UserExists(registerDto.Username))
            {
                return BadRequest("Username is taken");
            }

            // Initialize the AppUser object
            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                Email = registerDto.Email.ToLower(),
                Name = registerDto.Name,
                Gender = registerDto.Gender,
                BirthDate = registerDto.BirthDate,
                Location = registerDto.Location,
                IsActive = registerDto.IsActive,
                CreatedAt = registerDto.CreatedAt,
                Photos = Array.Empty<string>() // Initialize Photos
            };

            // Handle photo uploads if files are provided
            if (registerDto.Photos != null && registerDto.Photos.Any())
            {
                // Directory to save uploaded files
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                // Ensure the directory exists
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var uploadedUrls = new List<string>();

                foreach (var file in registerDto.Photos)
                {
                    // Validate the file
                    if (!file.ContentType.StartsWith("image/"))
                    {
                        return BadRequest($"Invalid file type for {file.FileName}. Only images are allowed.");
                    }

                    if (file.Length > 5 * 1024 * 1024) // 5 MB max size
                    {
                        return BadRequest($"File {file.FileName} exceeds the maximum allowed size of 5 MB.");
                    }

                    // Generate a unique file name
                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                    var filePath = Path.Combine(uploadPath, fileName);

                    // Save the file to the server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Generate the file URL
                    //var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
                    uploadedUrls.Add(fileName);
                }

                // Add uploaded URLs to the user's Photos property
                user.Photos = uploadedUrls.ToArray();
            }

            // Set default user type
            user.UserTypeId = 1;

            // Create the user
            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Assign default role
            await userManager.AddToRoleAsync(user, "Member");

            // Prepare the response DTO
            var response = new UserDTO
            {
                Token = await tokenService.CreateToken(user),
                Email = user.Email,
                Name = user.Name,
                GenderName = user.Gender == Gender.Male ? "Male" : "Female",
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UserId = user.Id,
                Photos = user.Photos.ToList()
            };

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


        [HttpGet("GetPhotos")]
        public async Task<ActionResult> GetPhotos(int? id)
        {
            if (id is null)
            {
                return BadRequest("Id is required.");
            }

            // Fetch the user
            var user = await _context.Users
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            if (user is null)
            {
                return Unauthorized("User not found or unauthorized.");
            }

            // Check if the user has photos
            if (user.Photos == null || !user.Photos.Any())
            {
                return NotFound("No photos found for this user.");
            }

            // Return the photos as a flat JSON array
            return Ok(user.Photos);
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

        //[HttpPost("UploadPhotos/{userId}")]
        //public async Task<IActionResult> UploadPhotos(int userId, [FromForm] List<IFormFile> files)
        //{
        //    if (files == null || !files.Any())
        //    {
        //        return BadRequest("No files were uploaded.");
        //    }

        //    // Fetch the user from the database
        //    var user = await _context.Users.FindAsync(userId);
        //    if (user == null)
        //    {
        //        return NotFound("User not found.");
        //    }

        //    // Initialize Photos if null
        //    user.Photos ??= Array.Empty<string>();

        //    // Directory to save the uploaded files (e.g., wwwroot/uploads)
        //    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

        //    if (!Directory.Exists(uploadPath))
        //    {
        //        Directory.CreateDirectory(uploadPath);
        //    }

        //    // List to store URLs of uploaded files
        //    var uploadedUrls = new List<string>();

        //    foreach (var file in files)
        //    {
        //        // Validate file type and size (example: allow only image files and max size 5 MB)
        //        if (!file.ContentType.StartsWith("image/"))
        //        {
        //            return BadRequest($"Invalid file type for {file.FileName}. Only images are allowed.");
        //        }

        //        if (file.Length > 5 * 1024 * 1024)
        //        {
        //            return BadRequest($"File {file.FileName} exceeds the maximum allowed size of 5 MB.");
        //        }

        //        // Generate a unique file name
        //        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";

        //        // Full path to save the file
        //        var filePath = Path.Combine(uploadPath, fileName);

        //        // Save the file
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }

        //        // Generate URL to access the uploaded file
        //        var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
        //        uploadedUrls.Add(fileUrl);
        //    }

        //    // Add the uploaded URLs to the user's Photos array
        //    var updatedPhotos = user.Photos.ToList();
        //    updatedPhotos.AddRange(uploadedUrls);
        //    user.Photos = updatedPhotos.ToArray();

        //    // Save changes to the database
        //    _context.Users.Update(user);
        //    await _context.SaveChangesAsync();

        //    return Ok(new
        //    {
        //        message = "Photos uploaded successfully.",
        //        photos = user.Photos
        //    });
        //}


        private async Task<bool> UserExists(string username)
        {
            return await userManager.Users.AnyAsync(x => x.NormalizedUserName == username.ToUpper()); // Bob != bob
        }
    }


}
