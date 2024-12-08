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
    public class ProfileQuestionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProfileQuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all questions for a user
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ProfileQuestionDto>>> GetUserQuestions(int userId)
        {
            var questions = await _context.ProfileQuestions
                .Where(q => q.UserId == userId && q.IsActive)
                .Select(q => new ProfileQuestionDto
                {
                    QuestionId = q.QuestionId,
                    Question = q.Question,
                    CorrectAnswer = q.CorrectAnswer,
                    IsActive = q.IsActive
                })
                .ToListAsync();

            return Ok(questions);
        }

        // Create new question
        [HttpPost]
        public async Task<ActionResult<ProfileQuestionDto>> CreateQuestion(int userId, CreateProfileQuestionDto dto)
        {
            var question = new ProfileQuestion
            {
                UserId = userId,
                Question = dto.Question,
                CorrectAnswer = dto.CorrectAnswer,
                IsActive = true
            };

            _context.ProfileQuestions.Add(question);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserQuestions), new { userId = question.UserId },
                new ProfileQuestionDto
                {
                    QuestionId = question.QuestionId,
                    Question = question.Question,
                    CorrectAnswer = question.CorrectAnswer,
                    IsActive = question.IsActive
                });
        }

        // Answer a question
        [HttpPost("{questionId}/answer")]
        public async Task<ActionResult> AnswerQuestion(int questionId, int userId, AnswerQuestionDto dto)
        {
            var question = await _context.ProfileQuestions.FindAsync(questionId);

            if (question == null)
                return NotFound();

            question.UserAnswer = dto.UserAnswer;
            question.AnsweredAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(question.UserAnswer == question.CorrectAnswer);
        }

        // Toggle question active status
        [HttpPut("{questionId}/toggle")]
        public async Task<ActionResult> ToggleQuestion(int questionId)
        {
            var question = await _context.ProfileQuestions.FindAsync(questionId);

            if (question == null)
                return NotFound();

            question.IsActive = !question.IsActive;
            await _context.SaveChangesAsync();

            return Ok();
        }

        // Delete question
        [HttpDelete("{questionId}")]
        public async Task<ActionResult> DeleteQuestion(int questionId)
        {
            var question = await _context.ProfileQuestions.FindAsync(questionId);

            if (question == null)
                return NotFound();

            _context.ProfileQuestions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
