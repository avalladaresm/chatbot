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
						var key = Program.Configuration["Trello:key"];
            var token = Program.Configuration["Trello:token"];
						List<TrelloBoard> list = new List<TrelloBoard>();
						using (var httpClient = new HttpClient())
						{
								using (var response = await httpClient.GetAsync($"https://api.trello.com/1/members/me/boards?fields=name&key={key}&token={token}"))
								{
										string apiResponse = await response.Content.ReadAsStringAsync();
										list = JsonConvert.DeserializeObject<List<TrelloBoard>>(apiResponse);
								}
						}
						return Ok(list);
        }

        // GET: api/TrelloBoard/5
        [HttpGet("{id}/Cards")]
        public async Task<ActionResult<TrelloCard>> GetTrelloBoardCards(string id)
        {					
						var key = Program.Configuration["Trello:key"];
            var token = Program.Configuration["Trello:token"];
						List<TrelloCard> list = new List<TrelloCard>();
						using (var httpClient = new HttpClient())
						{
								using (var response = await httpClient.GetAsync($"https://api.trello.com/1/boards/{id}/cards?key={key}&token={token}"))
								{
										string apiResponse = await response.Content.ReadAsStringAsync();
										list = JsonConvert.DeserializeObject<List<TrelloCard>>(apiResponse);
								}
						}
						return Ok(list);
        }

				[HttpGet("{id}/Lists")]
        public async Task<ActionResult<TrelloBoard>> GetTrelloBoardLists(string id)
        {					
						var key = Program.Configuration["Trello:key"];
            var token = Program.Configuration["Trello:token"];
						List<TrelloBoard> list = new List<TrelloBoard>();
						using (var httpClient = new HttpClient())
						{
								using (var response = await httpClient.GetAsync($"https://api.trello.com/1/boards/{id}/lists?key={key}&token={token}"))
								{
										string apiResponse = await response.Content.ReadAsStringAsync();
										list = JsonConvert.DeserializeObject<List<TrelloBoard>>(apiResponse);
								}
						}
						return Ok(list);
        }

        // POST: api/TrelloBoard
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TrelloBoard>> PostTrelloBoard(TrelloBoard trelloBoard)
        {
						var key = Program.Configuration["Trello:key"];
            var token = Program.Configuration["Trello:token"];
            List<TrelloBoard> list = new List<TrelloBoard>();
						string apiResponse;
						using (var httpClient = new HttpClient())
						{
								var value = new Dictionary<string, string>
								{
									{"name", trelloBoard.Name}
								};
							var content = new FormUrlEncodedContent(value);

								using (var response = await httpClient.PostAsync($"https://api.trello.com/1/boards/?key={key}&token={token}", content))
								{
										apiResponse = await response.Content.ReadAsStringAsync();									
								}
						}
						return Ok(apiResponse);
        }
    }
}
