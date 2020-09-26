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

        // PUT: api/TrelloCard/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrelloCard(string id, TrelloCard trelloCard)
        {
					  var key = Program.Configuration["Trello:key"];
            var token = Program.Configuration["Trello:token"];
						List<TrelloCard> list = new List<TrelloCard>();
						string apiResponse;
						using (var httpClient = new HttpClient())
						{
								var value = new Dictionary<string, string>();
								if (String.Empty != trelloCard.IdList)
									value.Add("idList", trelloCard.IdList);
								if(String.Empty != trelloCard.Desc)
									value.Add("desc", trelloCard.Desc);
								var content = new FormUrlEncodedContent(value);
								Console.WriteLine(trelloCard.Desc);
								Console.WriteLine(trelloCard.IdList);
								using (var response = await httpClient.PutAsync($"https://api.trello.com/1/cards/{id}?key={key}&token={token}", content))
								{
										apiResponse = await response.Content.ReadAsStringAsync();									
								}
						}
						return Ok(apiResponse);
        }

				

        // POST: api/TrelloCard
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TrelloCard>> PostTrelloCard(TrelloCard trelloCard)
        {
						var key = Program.Configuration["Trello:key"];
            var token = Program.Configuration["Trello:token"];
            List<TrelloCard> list = new List<TrelloCard>();
						string apiResponse;
						using (var httpClient = new HttpClient())
						{
								var value = new Dictionary<string, string>
								{
									{"idList", trelloCard.IdList},
									{"name", trelloCard.Name},
									{"desc", trelloCard.Desc}
								};
							 	var content = new FormUrlEncodedContent(value);

								using (var response = await httpClient.PostAsync($"https://api.trello.com/1/cards?key={key}&token={token}", content))
								{
										apiResponse = await response.Content.ReadAsStringAsync();									
								}
						}
						return Ok(apiResponse);
        }
    }
}
