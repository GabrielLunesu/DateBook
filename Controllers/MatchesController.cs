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
    public class MatchesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Matches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchDTO>>> GetMatches()
        {
            return await _context.Matches
                .Include(m => m.MatchedUser)
                .Select(match => new MatchDTO
                {
                    MatchId = match.MatchId,
                    UserId = match.UserId,
                    MatchedUserId = match.MatchedUserId,
                    MatchPercentage = match.MatchPercentage,
                    Status = match.Status,
                    CreatedAt = match.CreatedAt,
                    MatchedUserName = match.MatchedUser.Name,
                    MatchedUserPhoto = match.MatchedUser.Photos.FirstOrDefault()
                }).ToListAsync();
        }

        // GET: api/Matches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchDTO>> GetMatch(int id)
        {
            var match = await _context.Matches
                .Include(m => m.MatchedUser)
                .FirstOrDefaultAsync(m => m.MatchId == id);

            if (match == null)
            {
                return NotFound();
            }

            return new MatchDTO
            {
                MatchId = match.MatchId,
                UserId = match.UserId,
                MatchedUserId = match.MatchedUserId,
                MatchPercentage = match.MatchPercentage,
                Status = match.Status,
                CreatedAt = match.CreatedAt,
                MatchedUserName = match.MatchedUser.Name,
                MatchedUserPhoto = match.MatchedUser.Photos.FirstOrDefault()
            };
        }

        // PUT: api/Matches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatch(int id, UpdateMatchDTO updateMatchDTO)
        {
            var match = await _context.Matches.FindAsync(id);
            
            if (match == null)
            {
                return NotFound();
            }

            match.Status = updateMatchDTO.Status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
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

        // POST: api/Matches
        [HttpPost]
        public async Task<ActionResult<MatchDTO>> PostMatch(CreateMatchDTO createMatchDTO)
        {
            var match = new Match
            {
                UserId = createMatchDTO.UserId,
                MatchedUserId = createMatchDTO.MatchedUserId,
                MatchPercentage = createMatchDTO.MatchPercentage,
                Status = "Pending", // Default status
                CreatedAt = DateTime.UtcNow
            };

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            // Load the matched user for the response
            await _context.Entry(match)
                .Reference(m => m.MatchedUser)
                .LoadAsync();

            var matchDTO = new MatchDTO
            {
                MatchId = match.MatchId,
                UserId = match.UserId,
                MatchedUserId = match.MatchedUserId,
                MatchPercentage = match.MatchPercentage,
                Status = match.Status,
                CreatedAt = match.CreatedAt,
                MatchedUserName = match.MatchedUser.Name,
                MatchedUserPhoto = match.MatchedUser.Photos.FirstOrDefault()
            };

            return CreatedAtAction(
                nameof(GetMatch), 
                new { id = match.MatchId }, 
                matchDTO
            );
        }

        // DELETE: api/Matches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.MatchId == id);
        }
    }
}
