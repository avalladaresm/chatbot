using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using chatbotapi.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace chatbotapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly ChatBotContext _context;

        public MeetingController(ChatBotContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Meeting>> PostMeeting(Meeting meeting)
        {
            _context.Meeting.Add(meeting);
            await _context.SaveChangesAsync();

            return Ok(meeting);
        }
    }
}
