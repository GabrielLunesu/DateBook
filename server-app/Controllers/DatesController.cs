using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatingApp.Data;
using DatingApp.Models;

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
        public async Task<ActionResult<IEnumerable<Date>>> GetDates()
        {
            return await _context.Dates.ToListAsync();
        }

        // GET: api/Dates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Date>> GetDate(int id)
        {
            var date = await _context.Dates.FindAsync(id);

            if (date == null)
            {
                return NotFound();
            }

            return date;
        }

        // PUT: api/Dates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDate(int id, Date date)
        {
            if (id != date.DateId)
            {
                return BadRequest();
            }

            _context.Entry(date).State = EntityState.Modified;

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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Date>> PostDate(Date date)
        {
            _context.Dates.Add(date);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDate", new { id = date.DateId }, date);
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
