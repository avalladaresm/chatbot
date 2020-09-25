using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using chatbotapi.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace chatbotapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrelloCardController : ControllerBase
    {
        private readonly ChatBotContext _context;

        public TrelloCardController(ChatBotContext context)
        {
            _context = context;
        }

        // GET: api/TrelloCard
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrelloCard>>> GetTrelloCard()
        {
            return await _context.TrelloCard.ToListAsync();
        }

        // GET: api/TrelloCard/5
        [HttpGet("{id}/{boardId}")]
        public async Task<ActionResult<TrelloCard>> GetTrelloCards(string id, string boardId)
        {
            List<TrelloBoard> list = new List<TrelloBoard>();
						using (var httpClient = new HttpClient())
						{
								using (var response = await httpClient.GetAsync("https://api.trello.com/1/members/me/boards?fields=name&key=497ef583ac14260990a0da4666ba3ca1&token=c3bb539a50ffa542ed936f62debd6e76c777ffb6ea5a101a76c7fb35b050861d"))
								{
										string apiResponse = await response.Content.ReadAsStringAsync();
										list = JsonConvert.DeserializeObject<List<TrelloBoard>>(apiResponse);
								}
						}
						return Ok(list);
        }

        // PUT: api/TrelloCard/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrelloCard(string id, TrelloCard trelloCard)
        {

            return NoContent();
        }

        // POST: api/TrelloCard
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TrelloCard>> PostTrelloCard(TrelloCard trelloCard)
        {
            List<TrelloBoard> list = new List<TrelloBoard>();
						string apiResponse;
						using (var httpClient = new HttpClient())
						{
								var value = new Dictionary<string, string>
								{
									{"idList", trelloCard.IdList},
									{"name", trelloCard.Name}
								};
							 	var content = new FormUrlEncodedContent(value);

								using (var response = await httpClient.PostAsync($"https://api.trello.com/1/cards?key=497ef583ac14260990a0da4666ba3ca1&token=c3bb539a50ffa542ed936f62debd6e76c777ffb6ea5a101a76c7fb35b050861d", content))
								{
										apiResponse = await response.Content.ReadAsStringAsync();									
								}
						}
						return Ok(apiResponse);
        }
    }
}
