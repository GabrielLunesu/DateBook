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
    public class ChatsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Chats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatDTO>>> GetChats()
        {
            return await _context.Chats.Select(chat => new ChatDTO
            {
                ChatId = chat.ChatId,
                UserId = chat.UserId,
                ChatUserId = chat.ChatUserId,
                Status = chat.Status,
                LastMessage = chat.LastMessage
            }).ToListAsync();
        }

        // GET: api/Chats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatDetailDTO>> GetChat(int id)
        {
            var chat = await _context.Chats
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.ChatId == id);

            if (chat == null)
            {
                return NotFound();
            }

            return new ChatDetailDTO
            {
                ChatId = chat.ChatId,
                UserId = chat.UserId,
                ChatUserId = chat.ChatUserId,
                Status = chat.Status,
                LastMessage = chat.LastMessage,
                Messages = chat.Messages.Select(m => new MessageDTO
                {
                    MessageId = m.MessageId,
                    Content = m.Content,
                    Timestamp = m.Timestamp,
                    IsRead = m.IsRead
                }).ToList()
            };
        }

        // PUT: api/Chats/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChat(int id, UpdateChatDTO updateChatDTO)
        {
            var chat = await _context.Chats.FindAsync(id);
            
            if (chat == null)
            {
                return NotFound();
            }

            chat.Status = updateChatDTO.Status;
            chat.LastMessage = updateChatDTO.LastMessage;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatExists(id))
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

        // POST: api/Chats
        [HttpPost]
        public async Task<ActionResult<ChatDTO>> PostChat(CreateChatDTO createChatDTO)
        {
            var chat = new Chat
            {
                UserId = createChatDTO.UserId,
                ChatUserId = createChatDTO.ChatUserId,
                Status = createChatDTO.Status,
                LastMessage = DateTime.UtcNow  // Set initial timestamp
            };

            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            var chatDTO = new ChatDTO
            {
                ChatId = chat.ChatId,
                UserId = chat.UserId,
                ChatUserId = chat.ChatUserId,
                Status = chat.Status,
                LastMessage = chat.LastMessage
            };

            return CreatedAtAction(
                nameof(GetChat), 
                new { id = chat.ChatId }, 
                chatDTO
            );
        }

        // DELETE: api/Chats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(int id)
        {
            var chat = await _context.Chats.FindAsync(id);
            if (chat == null)
            {
                return NotFound();
            }

            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChatExists(int id)
        {
            return _context.Chats.Any(e => e.ChatId == id);
        }
    }
}
