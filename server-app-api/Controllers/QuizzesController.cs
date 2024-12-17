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
    public class QuizzesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuizzesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Quizzes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDTO>>> GetQuizzes()
        {
            return await _context.Quizzes
                .Select(quiz => new QuizDTO
                {
                    QuizId = quiz.QuizId,
                    UserId = quiz.UserId,
                    AgePreference = quiz.AgePreference,
                    RelationshipType = quiz.RelationshipType,
                    SportImportance = quiz.SportImportance,
                    SocialLevel = quiz.SocialLevel,
                    WeekendActivity = quiz.WeekendActivity,
                    CompletedAt = quiz.CompletedAt
                }).ToListAsync();
        }

        // GET: api/Quizzes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizDTO>> GetQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);

            if (quiz == null)
            {
                return NotFound();
            }

            return new QuizDTO
            {
                QuizId = quiz.QuizId,
                UserId = quiz.UserId,
                AgePreference = quiz.AgePreference,
                RelationshipType = quiz.RelationshipType,
                SportImportance = quiz.SportImportance,
                SocialLevel = quiz.SocialLevel,
                WeekendActivity = quiz.WeekendActivity,
                CompletedAt = quiz.CompletedAt
            };
        }

        // PUT: api/Quizzes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(int id, UpdateQuizDTO updateQuizDTO)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            
            if (quiz == null)
            {
                return NotFound();
            }

            quiz.AgePreference = updateQuizDTO.AgePreference ?? quiz.AgePreference;
            quiz.RelationshipType = updateQuizDTO.RelationshipType ?? quiz.RelationshipType;
            quiz.SportImportance = updateQuizDTO.SportImportance ?? quiz.SportImportance;
            quiz.SocialLevel = updateQuizDTO.SocialLevel ?? quiz.SocialLevel;
            quiz.WeekendActivity = updateQuizDTO.WeekendActivity ?? quiz.WeekendActivity;
            quiz.CompletedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizExists(id))
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

        // POST: api/Quizzes
        [HttpPost]
        public async Task<ActionResult<QuizDTO>> PostQuiz(CreateQuizDTO createQuizDTO)
        {
            var quiz = new Quiz
            {
                UserId = createQuizDTO.UserId,
                AgePreference = createQuizDTO.AgePreference,
                RelationshipType = createQuizDTO.RelationshipType,
                SportImportance = createQuizDTO.SportImportance,
                SocialLevel = createQuizDTO.SocialLevel,
                WeekendActivity = createQuizDTO.WeekendActivity,
                CompletedAt = DateTime.UtcNow
            };

            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();

            var quizDTO = new QuizDTO
            {
                QuizId = quiz.QuizId,
                UserId = quiz.UserId,
                AgePreference = quiz.AgePreference,
                RelationshipType = quiz.RelationshipType,
                SportImportance = quiz.SportImportance,
                SocialLevel = quiz.SocialLevel,
                WeekendActivity = quiz.WeekendActivity,
                CompletedAt = quiz.CompletedAt
            };

            return CreatedAtAction(
                nameof(GetQuiz), 
                new { id = quiz.QuizId }, 
                quizDTO
            );
        }

        // DELETE: api/Quizzes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuizExists(int id)
        {
            return _context.Quizzes.Any(e => e.QuizId == id);
        }
    }
}
