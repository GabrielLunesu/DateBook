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
using System.Reflection.Metadata.Ecma335;
using DatingApp.Models.Enums;

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
        // [HttpGet("{id}")]
        // public async Task<ActionResult<MatchDTO>> GetMatch(int id)
        // {
        //     var match = await _context.Matches
        //         .Include(m => m.MatchedUser)
        //         .FirstOrDefaultAsync(m => m.MatchId == id);

        //     if (match == null)
        //     {
        //         return NotFound();
        //     }

        //     return new MatchDTO
        //     {
        //         MatchId = match.MatchId,
        //         UserId = match.UserId,
        //         MatchedUserId = match.MatchedUserId,
        //         MatchPercentage = match.MatchPercentage,
        //         Status = match.Status,
        //         CreatedAt = match.CreatedAt,
        //         MatchedUserName = match.MatchedUser.Name,
        //         MatchedUserPhoto = match.MatchedUser.Photos.FirstOrDefault()
        //     };
        // }

        // PUT: api/Matches/5
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutMatch(int id, UpdateMatchDTO updateMatchDTO)
        // {
        //     var match = await _context.Matches.FindAsync(id);

        //     if (match == null)
        //     {
        //         return NotFound();
        //     }

        //     match.Status = updateMatchDTO.Status;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!MatchExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // POST: api/Matches
        // [HttpPost]
        // public async Task<ActionResult<MatchDTO>> PostMatch(CreateMatchDTO createMatchDTO)
        // {
        //     var match = new Match
        //     {
        //         UserId = createMatchDTO.UserId,
        //         MatchedUserId = createMatchDTO.MatchedUserId,
        //         MatchPercentage = createMatchDTO.MatchPercentage,
        //         Status = "Pending", // Default status
        //         CreatedAt = DateTime.UtcNow
        //     };

        //     _context.Matches.Add(match);
        //     await _context.SaveChangesAsync();

        //     // Load the matched user for the response
        //     await _context.Entry(match)
        //         .Reference(m => m.MatchedUser)
        //         .LoadAsync();

        //     var matchDTO = new MatchDTO
        //     {
        //         MatchId = match.MatchId,
        //         UserId = match.UserId,
        //         MatchedUserId = match.MatchedUserId,
        //         MatchPercentage = match.MatchPercentage,
        //         Status = match.Status,
        //         CreatedAt = match.CreatedAt,
        //         MatchedUserName = match.MatchedUser.Name,
        //         MatchedUserPhoto = match.MatchedUser.Photos.FirstOrDefault()
        //     };

        //     return CreatedAtAction(
        //         nameof(GetMatch),
        //         new { id = match.MatchId },
        //         matchDTO
        //     );
        // }

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

        [HttpGet("CalculateMatchingCompatibility/{id}")]
        public async Task<IActionResult> CalculateMatchingCompatibility(int id)
        {
            // Fetching the user's gender
            var currentUser = await _context.Users.Where(u => u.Id == id).Select(u => new { u.Gender, u.Name }).FirstOrDefaultAsync();
            if (currentUser == null)
            {
                return BadRequest("Current user not found");
            }
            // fetch all users with different gender, excluding the current user
            var compatibleUsersList = await _context.Users.Where(u => u.Id != id && u.Gender != currentUser.Gender).ToListAsync();
            if (!compatibleUsersList.Any()) {
                return Ok("No compatible users found");
            }
            // step 1 Fetch the current user's quiz repsone
            var currentUserResponses = await _context.QuizResponses.Where(qr => qr.UserId == id).Select(qr => new { qr.QuizId, qr.UserResponse }).ToListAsync();

            if (!currentUserResponses.Any()) {
                return BadRequest("Current user has not submitted any quiz responses");

            }

            // step 2 Fetch the others user's quiz repsone
            var compatiableUserIds = compatibleUsersList.Select(u => u.Id).ToList();
            var otherUserResponses = await _context.QuizResponses.Where(qr => compatiableUserIds.
            Contains(qr.UserId)).Select(qr => new { qr.UserId, qr.QuizId, qr.UserResponse }).ToListAsync();

            //Prepare a list of compatible users
            var compatiableUsers = new List<object>();
            // iterate through each compatiable user to calcualte compatibility
            foreach (var user in compatibleUsersList)
            {
                //Get all quiz responses for the current compatible user
                var userResponses = otherUserResponses.Where(qr => qr.UserId == user.Id).ToList();
                if (!userResponses.Any()) { continue; }

                // find common questions between both users
                //user 1 ,1-1,2-1,3-0,4-1,......
                //user 2 ,1-1,2-1,3-0,4-1.....
                var commonQuestionIds = currentUserResponses.Select(cr => cr.QuizId).Intersect(userResponses.Select(ur => ur.QuizId)).ToList();
                if (!commonQuestionIds.Any()) { continue; }

                //count matching answers for common questions
                int matchingAnswers = 0;
                foreach (var questionId in commonQuestionIds) {

                    var currentUserAnswer = currentUserResponses.First(cr => cr.QuizId == questionId).UserResponse;
                    var otherUserAnswer = userResponses.First(cr => cr.QuizId == questionId).UserResponse;
                    if (currentUserAnswer == otherUserAnswer) { matchingAnswers++; }

                }
                //Calculate compatibility
                double compatibility = (double)matchingAnswers / 10 * 100;
                // Add to compatibility users if compatibility is 60% or higher
                if (compatibility >= 60) {
                    compatiableUsers.Add(new
                    {
                        UserId = user.Id,
                        Name = user.Name,
                        Compatibility = Math.Round(compatibility, 2),
                        MatchingAnswers = matchingAnswers,
                        TotalQuestions = 10,
                        Photos=user.Photos.ToList()
                    });
                }

               

            }
            return Ok(compatiableUsers);
        } 
    } 
}

