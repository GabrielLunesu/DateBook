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


            // fetch all quiz responses from users
            var quizResponses = _context.QuizResponses.ToList();

            // add the quiz answers 
            PopulateQuizAnswers(quizResponses);

            // we have to filter the responses to the current user
            var selectedUserResponse = quizResponses.FirstOrDefault(u => u.UserId == id);

            if (selectedUserResponse == null)
            {
                return BadRequest("User has not submitted the quiz yet");
            }

            // exclude the selected user response (eleminate the current user response from the list because that is not matching)
            var quizResponsesExcludingUser = quizResponses.Where(u => u.UserId != id).ToList();

            // list of compatible user that fit the criteria of 60% 
            var compatibleUsers = new List<object>();

            // Iterate the array

            foreach (var response in quizResponsesExcludingUser)
            {

                if (response.QuizAnswers.Count != selectedUserResponse.QuizAnswers.Count)
                {
                    continue;
                }


                int matchingQuestions = CalculateMatchingQuestions(selectedUserResponse, response);
                int totalQuestions = selectedUserResponse.QuizAnswers.Count;

                // calculate the match percentage
                double compatibility = (double)matchingQuestions / totalQuestions * 100;


                if (compatibility >= 60)
                {
                    compatibleUsers.Add(new
                    {
                        UserId = response.UserId,
                        Compatibility = compatibility
                    });
                }
            }

            return Ok(compatibleUsers);
        }

        // calculate the match percentage
        // 
        private int CalculateMatchingQuestions(QuizResponse userResponse1, QuizResponse userResponse2)
        {
            int matchingQuestions = 0;

            for (int i = 0; i < userResponse1.QuizAnswers.Count; i++)
            {
                if (userResponse1.QuizAnswers[i] == userResponse2.QuizAnswers[i])
                {
                    matchingQuestions++;
                }
            }

            return matchingQuestions;
        }

        // get responses and add all the responses to a list
        private void PopulateQuizAnswers(List<QuizResponse> quizResponses)
        {
            foreach (var response in quizResponses)
            {

                response.QuizAnswers.Add(response.UserResponse);
            }
        }
    }
}
