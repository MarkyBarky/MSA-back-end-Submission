using Microsoft.AspNetCore.Mvc;
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
        public PokemonController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
            {
                throw new ArgumentNullException(nameof(clientFactory));
            }
            _client = clientFactory.CreateClient("pokemon");
        }
        /// <summary>
        /// Adds two numbers together
        /// </summary>
        /// <param name="left">The number on the left, which must be a positive integer</param>
        /// <param name="right">The number on the right, which must be a positive integer</param>
        /// <returns>The sum of the input numbers</returns>
        [HttpGet]
        [Route("add")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetSum(int left, int right)
        {
            if (left < 0 || right < 0) return BadRequest("The inputs must be greater than zero");

            return Ok(left + right);
        }
        [HttpGet]
        [Route("GetPokemon")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetRawRedditHotPosts()
        {
            var res = await _client.GetAsync("pokemon/");
            var content = await res.Content.ReadAsStringAsync();
            return Ok(content);
        }

        /// <summary>
        /// Generates a random number
        /// </summary>
        /// <returns>A random number</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetNumber()
        {
            return Ok(new Random().NextDouble());
        }

        /// <summary>
        /// Demonstrates posting action
        /// </summary>
        /// <returns>A 201 Created response</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        public IActionResult DemonstratePost()
        {
            Console.WriteLine("I'm doing some work right now to create a new thing...");

            return Created(new Uri("https://www.google.com"), "Hi There");
        }

        /// <summary>
        /// Demonstrates put action
        /// </summary>
        /// <returns>A 201 Created Response></returns>
        [HttpPut]
        [ProducesResponseType(201)]
        public IActionResult DemonstratePut()
        {
            Console.WriteLine("I'm over-writing whatever was there in the first place...");

            return Created(new Uri("https://www.google.com"), "Hi There");
        }

        /// <summary>
        /// Demonstrates a delete action
        /// </summary>
        /// <returns>A 204 No Content Response</returns>
        [HttpDelete]
        [ProducesResponseType(204)]
        public IActionResult DemonstrateDelete()
        {
            Console.WriteLine("I'm removing something from the database...");

            return NoContent();
        }
    }
}