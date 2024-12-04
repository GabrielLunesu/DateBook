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
    public class MessagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessages()
        {
            return await _context.Messages
                .Select(message => new MessageDTO
                {
                    MessageId = message.MessageId,
                    ChatId = message.ChatId,
                    Content = message.Content,
                    Timestamp = message.Timestamp,
                    IsRead = message.IsRead
                }).ToListAsync();
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageDTO>> GetMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return new MessageDTO
            {
                MessageId = message.MessageId,
                ChatId = message.ChatId,
                Content = message.Content,
                Timestamp = message.Timestamp,
                IsRead = message.IsRead
            };
        }

        // PUT: api/Messages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(int id, UpdateMessageDTO updateMessageDTO)
        {
            var message = await _context.Messages.FindAsync(id);
            
            if (message == null)
            {
                return NotFound();
            }

            message.IsRead = updateMessageDTO.IsRead;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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

        // POST: api/Messages
        [HttpPost]
        public async Task<ActionResult<MessageDTO>> PostMessage(CreateMessageDTO createMessageDTO)
        {
            var message = new Message
            {
                ChatId = createMessageDTO.ChatId,
                Content = createMessageDTO.Content,
                Timestamp = DateTime.UtcNow,
                IsRead = false // Default value
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            var messageDTO = new MessageDTO
            {
                MessageId = message.MessageId,
                ChatId = message.ChatId,
                Content = message.Content,
                Timestamp = message.Timestamp,
                IsRead = message.IsRead
            };

            return CreatedAtAction(
                nameof(GetMessage), 
                new { id = message.MessageId }, 
                messageDTO
            );
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.MessageId == id);
        }
    }
}
