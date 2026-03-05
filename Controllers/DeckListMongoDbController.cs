using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Security.Cryptography.X509Certificates;
using YuGiOhDeckApi.Data;
using YuGiOhDeckApi.Models;
using YuGiOhDeckApi.Repositories;

namespace YuGiOhDeckApi.Controllers
{
    [Route("api/mongodb/[controller]")]
    [ApiController]
    public class DeckListMongoDbController : ControllerBase
    {
        /* =======================================
         * =========== MONGODB LOGIC =============
         ========================================*/
        private readonly IMongoCollection<DeckList>? _deckListCollection;
        private readonly MongoDbService _mongoDbService;

        public DeckListMongoDbController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        [HttpGet]
        public async Task<IEnumerable<DeckList>> Get()
        {
            return await _mongoDbService.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<DeckList> GetById(int id)
        {
            return await _mongoDbService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]DeckList deckList)
        {
            await _mongoDbService.CreateAsync(deckList);
            return CreatedAtAction(nameof(Get), new { id = deckList.Id }, deckList);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody]DeckList deckList, int id)
        {
            await _mongoDbService.UpdateByIdAsync(deckList, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            await _mongoDbService.DeleteByIdAsync(id);
            return NoContent();    
        }

        [HttpDelete("{title}")]
        public async Task<ActionResult> DeleteByTitle(string title)
        {
            await _mongoDbService.DeleteByTitleAsync(title);
            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<DeckList>>> GetByUserId(int userId)
        {
            // Use the service! It's the one that has the database connection.
            var decks = await _mongoDbService.GetByUserIdAsync(userId);

            if (decks == null)
            {
                return Ok(new List<DeckList>()); // Return empty list instead of null
            }

            return Ok(decks);
        }
    }
}
