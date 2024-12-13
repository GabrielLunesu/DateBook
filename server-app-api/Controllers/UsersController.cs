using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatingApp.Data;
using DatingApp.Models;
using DatingApp.DTOs;

namespace dating_app_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return await _context.Users
                .Select(user => new UserDTO
                {
                    Id = user.Id,
                    Token=string.Empty,
                    Email = user.Email,
                    Name = user.Name,
                    BirthDate = user.BirthDate,
                    Photos = user.Photos,
                    Location = user.Location,
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt
                }).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailDTO>> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.UserType)
                .Include(u => u.Profile)
                .Include(u => u.Quiz)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return new UserDetailDTO
            {
                Id = user.Id,
                Token = string.Empty,
                Email = user.Email,
                Name = user.Name,
                BirthDate = user.BirthDate,
                Photos = user.Photos,
                Location = user.Location,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UserTypeName = user.UserType?.Name,
                Profile = user.Profile == null ? null : new ProfileDTO
                {
                    ProfileId = user.Profile.ProfileId,
                    Bio = user.Profile.Bio,
                    Gender = user.Profile.Gender,
                    Preferences = user.Profile.Preferences,
                    MinAge = user.Profile.MinAge,
                    MaxAge = user.Profile.MaxAge,
                    LastActive = user.Profile.LastActive
                },
                Quiz = user.Quiz == null ? null : new QuizDTO
                {
                    QuizId = user.Quiz.QuizId,
                    AgePreference = user.Quiz.AgePreference,
                    RelationshipType = user.Quiz.RelationshipType,
                    SportImportance = user.Quiz.SportImportance,
                    SocialLevel = user.Quiz.SocialLevel,
                    WeekendActivity = user.Quiz.WeekendActivity,
                    CompletedAt = user.Quiz.CompletedAt
                }
            };
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UpdateUserDTO updateUserDTO)
        {
            var user = await _context.Users.FindAsync(id);
            
            if (user == null)
            {
                return NotFound();
            }

            user.Name = updateUserDTO.Name ?? user.Name;
            user.Photos = updateUserDTO.Photos ?? user.Photos;
            user.Location = updateUserDTO.Location ?? user.Location;
            user.IsActive = updateUserDTO.IsActive;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(CreateUserDTO createUserDTO)
        {
            var user = new AppUser
            {
                Email = createUserDTO.Email,
            
              
                Name = createUserDTO.Name,
                BirthDate = createUserDTO.BirthDate,
                Location = createUserDTO.Location,
                UserTypeId = createUserDTO.UserTypeId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                Photos = new string[] { }
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userDTO = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Token = string.Empty,
                Name = user.Name,
                BirthDate = user.BirthDate,
                Photos = user.Photos,
                Location = user.Location,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };

            return CreatedAtAction(
                nameof(GetUser), 
                new { id = user.Id }, 
                userDTO
            );
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
