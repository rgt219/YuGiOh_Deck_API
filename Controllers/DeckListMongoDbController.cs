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
            //return await _deckListCollection.Find(FilterDefinition<DeckList>.Empty).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<DeckList> GetById(int id)
        {
            return await _mongoDbService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(DeckList deckList)
        {
            await _mongoDbService.CreateAsync(deckList);
            return CreatedAtAction(nameof(Get), new { id = deckList.Id }, deckList);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(DeckList deckList, int id)
        {
            //var filter = Builders<DeckList>.Filter.Eq(x => x.Id, deckList.Id);

            //await _deckListCollection.ReplaceOneAsync(filter, deckList);
            //return Ok();
            await _mongoDbService.UpdateByIdAsync(deckList, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            //var filter = Builders<DeckList>.Filter.Eq(x => x.Id, id);
            //await _deckListCollection.DeleteOneAsync(filter);
            //return Ok();
            await _mongoDbService.DeleteByIdAsync(id);
            return NoContent();    
        }

        [HttpDelete("{title}")]
        public async Task<ActionResult> DeleteByTitle(string title)
        {
            //var filter = Builders<DeckList>.Filter.Eq(x => x.Id, id);
            //await _deckListCollection.DeleteOneAsync(filter);
            //return Ok();
            await _mongoDbService.DeleteByTitleAsync(title);
            return NoContent();
        }
    }
}
