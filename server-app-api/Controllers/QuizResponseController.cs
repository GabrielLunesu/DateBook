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
    public class QuizResponseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuizResponseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/QuizResponse
        [HttpPost]
        public async Task<ActionResult<QuizResponseDTO>> AnswerQuiz(CreateQuizResponseDTO createDto)
        {
            // Check if quiz exists
            var quiz = await _context.Quiz.FindAsync(createDto.QuizId);
            if (quiz == null || !quiz.Status)
            {
                return BadRequest("Quiz not found or inactive");
            }

            // Check if user already answered this quiz
            var existingResponse = await _context.QuizResponses
                .FirstOrDefaultAsync(r => r.QuizId == createDto.QuizId && r.UserId == createDto.UserId);

            if (existingResponse != null)
            {
                return BadRequest("User has already answered this quiz");
            }

            var response = new QuizResponse
            {
                QuizId = createDto.QuizId,
                UserId = createDto.UserId,
                UserResponse = createDto.UserResponse,
                CompletedAt = DateTime.UtcNow
            };

            _context.QuizResponses.Add(response);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetResponse), 
                new { id = response.Id }, 
                new QuizResponseDTO
                {
                    Id = response.Id,
                    QuizId = response.QuizId,
                    UserId = response.UserId,
                    UserResponse = response.UserResponse,
                    CompletedAt = response.CompletedAt
                });
        }

        // GET: api/QuizResponse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizResponseDTO>> GetResponse(int id)
        {
            var response = await _context.QuizResponses.FindAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            return new QuizResponseDTO
            {
                Id = response.Id,
                QuizId = response.QuizId,
                UserId = response.UserId,
                UserResponse = response.UserResponse,
                CompletedAt = response.CompletedAt
            };
        }

        // GET: api/QuizResponse/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<QuizResponseDTO>>> GetUserResponses(int userId)
        {
            var responses = await _context.QuizResponses
                .Where(r => r.UserId == userId)
                .Include(r => r.Quiz)  // Include Quiz details if needed
                .Select(r => new QuizResponseDTO
                {
                    Id = r.Id,
                    QuizId = r.QuizId,
                    UserId = r.UserId,
                    UserResponse = r.UserResponse,
                    CompletedAt = r.CompletedAt
                })
                .ToListAsync();

            return Ok(responses);
        }
    }
}
