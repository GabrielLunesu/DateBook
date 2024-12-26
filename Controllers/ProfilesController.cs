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
    public class ProfilesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Profiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfileDTO>>> GetProfiles()
        {
            return await _context.Profiles
                .Select(profile => new ProfileDTO
                {
                    ProfileId = profile.ProfileId,
                    UserId = profile.UserId,
                    Bio = profile.Bio,
                    Gender = profile.Gender,
                    Preferences = profile.Preferences,
                    MinAge = profile.MinAge,
                    MaxAge = profile.MaxAge,
                    LastActive = profile.LastActive
                }).ToListAsync();
        }

        // GET: api/Profiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileDTO>> GetProfile(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);

            if (profile == null)
            {
                return NotFound();
            }

            return new ProfileDTO
            {
                ProfileId = profile.ProfileId,
                UserId = profile.UserId,
                Bio = profile.Bio,
                Gender = profile.Gender,
                Preferences = profile.Preferences,
                MinAge = profile.MinAge,
                MaxAge = profile.MaxAge,
                LastActive = profile.LastActive
            };
        }

        // PUT: api/Profiles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile(int id, UpdateProfileDTO updateProfileDTO)
        {
            var profile = await _context.Profiles.FindAsync(id);
            
            if (profile == null)
            {
                return NotFound();
            }

            profile.Bio = updateProfileDTO.Bio ?? profile.Bio;
            profile.Gender = updateProfileDTO.Gender ?? profile.Gender;
            profile.Preferences = updateProfileDTO.Preferences ?? profile.Preferences;
            profile.MinAge = updateProfileDTO.MinAge ?? profile.MinAge;
            profile.MaxAge = updateProfileDTO.MaxAge ?? profile.MaxAge;
            profile.LastActive = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileExists(id))
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

        // POST: api/Profiles
        [HttpPost]
        public async Task<ActionResult<ProfileDTO>> PostProfile(CreateProfileDTO createProfileDTO)
        {
            var profile = new Profile
            {
                UserId = createProfileDTO.UserId,
                Bio = createProfileDTO.Bio,
                Gender = createProfileDTO.Gender,
                Preferences = createProfileDTO.Preferences,
                MinAge = createProfileDTO.MinAge,
                MaxAge = createProfileDTO.MaxAge,
                LastActive = DateTime.UtcNow
            };

            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();

            var profileDTO = new ProfileDTO
            {
                ProfileId = profile.ProfileId,
                UserId = profile.UserId,
                Bio = profile.Bio,
                Gender = profile.Gender,
                Preferences = profile.Preferences,
                MinAge = profile.MinAge,
                MaxAge = profile.MaxAge,
                LastActive = profile.LastActive
            };

            return CreatedAtAction(
                nameof(GetProfile), 
                new { id = profile.ProfileId }, 
                profileDTO
            );
        }

        // DELETE: api/Profiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfileExists(int id)
        {
            return _context.Profiles.Any(e => e.ProfileId == id);
        }
    }
}
