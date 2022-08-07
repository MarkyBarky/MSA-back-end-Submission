using Microsoft.AspNetCore.Mvc;
using MSA.backend.Api.Model;
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
        [Route("addMoveByName")]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> getMoves(string name)
        {
            var res = await _client.GetAsync("pokemon/" + name);
            var content = await res.Content.ReadAsStringAsync();
            if (content == "Not Found")
            {
                return NotFound("not a pokemon!");
            }
            int begin = content.IndexOf("moves") + 8;
            string result1 = content.Substring(begin);
            int finish = result1.IndexOf("version_group_details");
            var comb = result1.Substring(0, finish);
            int start = comb.IndexOf("name\":\"") + ("name\":\"").Length;
            int end = comb.Substring(start).IndexOf("\",");
            var comb2 = comb.Substring(start, end);

            Move skill = new Move { move = comb2};
            
            if (_repo.GetMoveByName(skill.move) != null)
            {
                return NotFound("already in the skillset!");
            }
            Move ability = _repo.addMoves(skill);


            return Created(new Uri("https://localhost:44346/Pokemon/Pokemon_getMoves?name=" + skill.move),
                    ability);

        }
        [HttpGet]
        [Route("getMoves")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetMovesByName(string name)
        {
            Move moves = _repo.GetMoveByName(name);
            if (moves == null)
                return NotFound("No pokemon with the move " + name.ToString());
            else
            {
                return Ok(moves);
            }

        }

        [HttpDelete]
        [Route("deleteMove")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult deleteMove(string name)
        {
            Move m = _repo.GetMoveByName(name);
            if (m != null)
            {
                _repo.deleteMoves(m);
                return NoContent();
            }
            else
            {
                return BadRequest("The move " + name + " does not exist.");
            }
        }




    }
}