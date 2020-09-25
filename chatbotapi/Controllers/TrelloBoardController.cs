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
    public class TrelloBoardController : ControllerBase
    {
        private readonly ChatBotContext _context;

        public TrelloBoardController(ChatBotContext context)
        {
            _context = context;
        }

        // GET: api/TrelloBoard
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrelloBoard>>> GetTrelloBoard()
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

        // GET: api/TrelloBoard/5
        [HttpGet("{id}/Cards")]
        public async Task<ActionResult<TrelloBoard>> GetTrelloBoardCards(string id)
        {					
						List<TrelloBoard> list = new List<TrelloBoard>();
						using (var httpClient = new HttpClient())
						{
								using (var response = await httpClient.GetAsync($"https://api.trello.com/1/boards/{id}/cards?key=497ef583ac14260990a0da4666ba3ca1&token=c3bb539a50ffa542ed936f62debd6e76c777ffb6ea5a101a76c7fb35b050861d"))
								{
										string apiResponse = await response.Content.ReadAsStringAsync();
										list = JsonConvert.DeserializeObject<List<TrelloBoard>>(apiResponse);
								}
						}
						return Ok(list);
        }

				[HttpGet("{id}/Lists")]
        public async Task<ActionResult<TrelloBoard>> GetTrelloBoardLists(string id)
        {					
						List<TrelloBoard> list = new List<TrelloBoard>();
						using (var httpClient = new HttpClient())
						{
								using (var response = await httpClient.GetAsync($"https://api.trello.com/1/boards/{id}/lists?key=497ef583ac14260990a0da4666ba3ca1&token=c3bb539a50ffa542ed936f62debd6e76c777ffb6ea5a101a76c7fb35b050861d"))
								{
										string apiResponse = await response.Content.ReadAsStringAsync();
										list = JsonConvert.DeserializeObject<List<TrelloBoard>>(apiResponse);
								}
						}
						return Ok(list);
        }

        // PUT: api/TrelloBoard/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
/*         [HttpPut("{id}")]
        public async Task<IActionResult> PutTrelloBoard(string id, TrelloBoard trelloBoard)
        {
            return NoContent();
        } */

        // POST: api/TrelloBoard
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TrelloBoard>> PostTrelloBoard(TrelloBoard trelloBoard)
        {
            List<TrelloBoard> list = new List<TrelloBoard>();
						string apiResponse;
						using (var httpClient = new HttpClient())
						{
								var value = new Dictionary<string, string>
								{
									{"name", trelloBoard.Name}
								};
							var content = new FormUrlEncodedContent(value);

								using (var response = await httpClient.PostAsync($"https://api.trello.com/1/boards/?key=497ef583ac14260990a0da4666ba3ca1&token=c3bb539a50ffa542ed936f62debd6e76c777ffb6ea5a101a76c7fb35b050861d", content))
								{
										apiResponse = await response.Content.ReadAsStringAsync();									
								}
						}
						return Ok(apiResponse);
        }
    }
}
