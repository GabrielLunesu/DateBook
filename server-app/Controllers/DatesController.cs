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
    public class DatesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Dates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DateDTO>>> GetDates()
        {
            return await _context.Dates
                .Include(d => d.DateUser)
                .Select(date => new DateDTO
                {
                    DateId = date.DateId,
                    UserId = date.UserId,
                    DateUserId = date.DateUserId,
                    Location = date.Location,
                    DateTime = date.DateTime,
                    Status = date.Status,
                    DateUserName = date.DateUser.Name,
                    DateUserPhoto = date.DateUser.Photos.FirstOrDefault()
                }).ToListAsync();
        }

        // GET: api/Dates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DateDTO>> GetDate(int id)
        {
            var date = await _context.Dates
                .Include(d => d.DateUser)
                .FirstOrDefaultAsync(d => d.DateId == id);

            if (date == null)
            {
                return NotFound();
            }

            return new DateDTO
            {
                DateId = date.DateId,
                UserId = date.UserId,
                DateUserId = date.DateUserId,
                Location = date.Location,
                DateTime = date.DateTime,
                Status = date.Status,
                DateUserName = date.DateUser.Name,
                DateUserPhoto = date.DateUser.Photos.FirstOrDefault()
            };
        }

        // PUT: api/Dates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDate(int id, UpdateDateDTO updateDateDTO)
        {
            var date = await _context.Dates.FindAsync(id);
            
            if (date == null)
            {
                return NotFound();
            }

            date.Location = updateDateDTO.Location ?? date.Location;
            date.DateTime = updateDateDTO.DateTime;
            date.Status = updateDateDTO.Status ?? date.Status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DateExists(id))
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

        // POST: api/Dates
        [HttpPost]
        public async Task<ActionResult<DateDTO>> PostDate(CreateDateDTO createDateDTO)
        {
            var date = new Date
            {
                UserId = createDateDTO.UserId,
                DateUserId = createDateDTO.DateUserId,
                Location = createDateDTO.Location,
                DateTime = createDateDTO.DateTime,
                Status = "Pending" // Default status
            };

            _context.Dates.Add(date);
            await _context.SaveChangesAsync();

            // Load the date user for the response
            await _context.Entry(date)
                .Reference(d => d.DateUser)
                .LoadAsync();

            var dateDTO = new DateDTO
            {
                DateId = date.DateId,
                UserId = date.UserId,
                DateUserId = date.DateUserId,
                Location = date.Location,
                DateTime = date.DateTime,
                Status = date.Status,
                DateUserName = date.DateUser.Name,
                DateUserPhoto = date.DateUser.Photos.FirstOrDefault()
            };

            return CreatedAtAction(
                nameof(GetDate), 
                new { id = date.DateId }, 
                dateDTO
            );
        }

        // DELETE: api/Dates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDate(int id)
        {
            var date = await _context.Dates.FindAsync(id);
            if (date == null)
            {
                return NotFound();
            }

            _context.Dates.Remove(date);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DateExists(int id)
        {
            return _context.Dates.Any(e => e.DateId == id);
        }
    }
}
