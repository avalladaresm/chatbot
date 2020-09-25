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
    public class TrelloListController : ControllerBase
    {
        private readonly ChatBotContext _context;

        public TrelloListController(ChatBotContext context)
        {
            _context = context;
        }

        // GET: api/TrelloList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrelloList>>> GetTrelloList()
        {
            return await _context.TrelloList.ToListAsync();
        }

        // GET: api/TrelloList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrelloList>> GetTrelloList(string id)
        {
            var trelloList = await _context.TrelloList.FindAsync(id);

            if (trelloList == null)
            {
                return NotFound();
            }

            return trelloList;
        }

        // PUT: api/TrelloList/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrelloList(string id, TrelloList trelloList)
        {
            if (id != trelloList.Id)
            {
                return BadRequest();
            }

            _context.Entry(trelloList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrelloListExists(id))
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

        // POST: api/TrelloList
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TrelloList>> PostTrelloList(TrelloList trelloList)
        {
            _context.TrelloList.Add(trelloList);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TrelloListExists(trelloList.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTrelloList", new { id = trelloList.Id }, trelloList);
        }

        // DELETE: api/TrelloList/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TrelloList>> DeleteTrelloList(string id)
        {
            var trelloList = await _context.TrelloList.FindAsync(id);
            if (trelloList == null)
            {
                return NotFound();
            }

            _context.TrelloList.Remove(trelloList);
            await _context.SaveChangesAsync();

            return trelloList;
        }

        private bool TrelloListExists(string id)
        {
            return _context.TrelloList.Any(e => e.Id == id);
        }
    }
}
