using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using chatbotapi.Models;

namespace chatbotapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatItemsController : ControllerBase
    {
        private readonly ChatBotContext _context;

        public ChatItemsController(ChatBotContext context)
        {
            _context = context;
        }

        // GET: api/ChatItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatItem>>> GetChatItems()
        {
            return await _context.ChatItems.ToListAsync();
        }

        // GET: api/ChatItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatItem>> GetChatItem(int id)
        {
            var chatItem = await _context.ChatItems.FindAsync(id);

            if (chatItem == null)
            {
                return NotFound();
            }

            return chatItem;
        }

        // PUT: api/ChatItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChatItem(int id, ChatItem chatItem)
        {
            if (id != chatItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(chatItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatItemExists(id))
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

        [HttpPost]
        public async Task<ActionResult<ChatItem>> PostChatItem(ChatItem chatItem)
        {
            _context.ChatItems.Add(chatItem);
            await _context.SaveChangesAsync();

    				return CreatedAtAction(nameof(GetChatItem), new { id = chatItem.Id }, chatItem);
        }

        // DELETE: api/ChatItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ChatItem>> DeleteChatItem(int id)
        {
            var chatItem = await _context.ChatItems.FindAsync(id);
            if (chatItem == null)
            {
                return NotFound();
            }

            _context.ChatItems.Remove(chatItem);
            await _context.SaveChangesAsync();

            return chatItem;
        }

        private bool ChatItemExists(int id)
        {
            return _context.ChatItems.Any(e => e.Id == id);
        }
    }
}
