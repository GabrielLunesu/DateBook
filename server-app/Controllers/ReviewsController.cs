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
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviews()
        {
            return await _context.Reviews
                .Include(r => r.ReviewedUser)
                .Select(review => new ReviewDTO
                {
                    ReviewId = review.ReviewId,
                    UserId = review.UserId,
                    ReviewedUserId = review.ReviewedUserId,
                    Rating = review.Rating,
                    Comment = review.Comment,
                    CreatedAt = review.CreatedAt,
                    ReviewedUserName = review.ReviewedUser.Name,
                    ReviewedUserPhoto = review.ReviewedUser.Photos.FirstOrDefault()
                }).ToListAsync();
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDTO>> GetReview(int id)
        {
            var review = await _context.Reviews
                .Include(r => r.ReviewedUser)
                .FirstOrDefaultAsync(r => r.ReviewId == id);

            if (review == null)
            {
                return NotFound();
            }

            return new ReviewDTO
            {
                ReviewId = review.ReviewId,
                UserId = review.UserId,
                ReviewedUserId = review.ReviewedUserId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt,
                ReviewedUserName = review.ReviewedUser.Name,
                ReviewedUserPhoto = review.ReviewedUser.Photos.FirstOrDefault()
            };
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, UpdateReviewDTO updateReviewDTO)
        {
            var review = await _context.Reviews.FindAsync(id);
            
            if (review == null)
            {
                return NotFound();
            }

            review.Rating = updateReviewDTO.Rating;
            review.Comment = updateReviewDTO.Comment;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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

        // POST: api/Reviews
        [HttpPost]
        public async Task<ActionResult<ReviewDTO>> PostReview(CreateReviewDTO createReviewDTO)
        {
            var review = new Review
            {
                UserId = createReviewDTO.UserId,
                ReviewedUserId = createReviewDTO.ReviewedUserId,
                Rating = createReviewDTO.Rating,
                Comment = createReviewDTO.Comment,
                CreatedAt = DateTime.UtcNow
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            // Load the reviewed user for the response
            await _context.Entry(review)
                .Reference(r => r.ReviewedUser)
                .LoadAsync();

            var reviewDTO = new ReviewDTO
            {
                ReviewId = review.ReviewId,
                UserId = review.UserId,
                ReviewedUserId = review.ReviewedUserId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt,
                ReviewedUserName = review.ReviewedUser.Name,
                ReviewedUserPhoto = review.ReviewedUser.Photos.FirstOrDefault()
            };

            return CreatedAtAction(
                nameof(GetReview), 
                new { id = review.ReviewId }, 
                reviewDTO
            );
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.ReviewId == id);
        }
    }
}
