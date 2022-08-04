using Microsoft.AspNetCore.Mvc;
using MSA.backend.Data;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MSA.backend.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly iDbRepo _repo;
        public PokemonController(IHttpClientFactory clientFactory, iDbRepo repo)
        {
            if (clientFactory is null)
            {
                throw new ArgumentNullException(nameof(clientFactory));
            }
            _client = clientFactory.CreateClient("pokemon");
            _repo = repo;
        }
        
        
        [HttpGet]
        [Route("GetPokemon")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetRawPokemonData()
        {
            var res = await _client.GetAsync("pokemon/");
            var content = await res.Content.ReadAsStringAsync();
            return Ok(content);
        }


        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> getMoves(string name)
        {
            var res = await _client.GetAsync("pokemon/" + name);
            var content = await res.Content.ReadAsStringAsync();
            if (content == "Not Found")
            {
                return NotFound("not a move!");
            }
            int begin = content.IndexOf("moves") + 8;
            string result1 = content.Substring(begin);
            int finish = result1.IndexOf("version_group_details");
            var comb = result1.Substring(begin, finish);
            int start = result1.IndexOf("name\":\"") + ("name\":\"").Length;
            int end = result1.IndexOf("\",");
            var comb2 = comb.Substring(start, end);

            return Created(new Uri("https://www.google.com"), comb2);



        }

        [HttpPut]
        [ProducesResponseType(201)]
        public IActionResult DemonstratePut()
        {
            Console.WriteLine("I'm over-writing whatever was there in the first place...");

            return Created(new Uri("https://www.google.com"), "Hi There");
        }

        
        [HttpDelete]
        [ProducesResponseType(204)]
        public IActionResult DemonstrateDelete()
        {
            Console.WriteLine("I'm removing something from the database...");

            return NoContent();
        }
    }
}