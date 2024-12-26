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
    public class QuizController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuizController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Quiz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDTO>>> GetQuizzes()
        {
            var quizzes = await _context.Quiz
                .Include(q => q.QuizResponses)
                .Select(q => new QuizDTO
                {
                    Id = q.Id,
                    Question = q.Question,
                    Status = q.Status,
                    Responses = q.QuizResponses.Select(r => new QuizResponseDTO
                    {
                        Id = r.Id,
                        QuizId = r.QuizId,
                        UserId = r.UserId,
                        UserResponse = r.UserResponse,
                        CompletedAt = r.CompletedAt
                    }).ToList()
                })
                .ToListAsync();

            return Ok(quizzes);
        }

        // GET: api/Quiz/active
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<QuizDTO>>> GetActiveQuizzes()
        {
            var quizzes = await _context.Quiz
                .Where(q => q.Status)
                .Select(q => new QuizDTO
                {
                    Id = q.Id,
                    Question = q.Question,
                    Status = q.Status,
                    Responses = q.QuizResponses.Select(r => new QuizResponseDTO
                    {
                        Id = r.Id,
                        QuizId = r.QuizId,
                        UserId = r.UserId,
                        UserResponse = r.UserResponse,
                        CompletedAt = r.CompletedAt
                    }).ToList()
                })
                .ToListAsync();

            return Ok(quizzes);
        }

        // GET: api/Quiz/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizDTO>> GetQuiz(int id)
        {
            var quiz = await _context.Quiz
                .Include(q => q.QuizResponses)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (quiz == null)
            {
                return NotFound();
            }

            return new QuizDTO
            {
                Id = quiz.Id,
                Question = quiz.Question,
                Status = quiz.Status,
                Responses = quiz.QuizResponses.Select(r => new QuizResponseDTO
                {
                    Id = r.Id,
                    QuizId = r.QuizId,
                    UserId = r.UserId,
                    UserResponse = r.UserResponse,
                    CompletedAt = r.CompletedAt
                }).ToList()
            };
        }

        // POST: api/Quiz
        [HttpPost]
        public async Task<ActionResult<QuizDTO>> CreateQuiz(CreateQuizDTO createQuizDto)
        {
            var quiz = new Quiz
            {
                Question = createQuizDto.Question,
                Status = createQuizDto.Status
            };

            _context.Quiz.Add(quiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetQuiz), 
                new { id = quiz.Id }, 
                new QuizDTO 
                { 
                    Id = quiz.Id,
                    Question = quiz.Question,
                    Status = quiz.Status,
                    Responses = new List<QuizResponseDTO>()
                }
            );
        }

        // PUT: api/Quiz/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuiz(int id, UpdateQuizDTO updateQuizDto)
        {
            var quiz = await _context.Quiz.FindAsync(id);

            if (quiz == null)
            {
                return NotFound();
            }

            quiz.Question = updateQuizDto.Question;
            quiz.Status = updateQuizDto.Status;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Quiz/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            var quiz = await _context.Quiz.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            _context.Quiz.Remove(quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
