using MongoDB.Driver;
using MongoDB.Bson;
using YuGiOhDeckApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace YuGiOhDeckApi.Data
{
    public class MongoDbService
    {
        private readonly IMongoCollection<DeckList> _deckListCollection;

        public MongoDbService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            Console.WriteLine("CONNECTION URI: " + mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _deckListCollection = database.GetCollection<DeckList>(mongoDBSettings.Value.CollectionName);
        }

        public async Task CreateAsync(DeckList deckList)
        {
            await _deckListCollection.InsertOneAsync(deckList);
            return;
        }

        public async Task<IEnumerable<DeckList>> GetAsync()
        {
            return await _deckListCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<DeckList> GetByIdAsync(int id)
        {
            return await _deckListCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<DeckList?> GetByNameAsync(string title)
        {
            return await _deckListCollection.Find(x => x.Title.ToLower() == title.ToLower()).FirstOrDefaultAsync();
        }

        public async Task UpdateByIdAsync(DeckList deck, int id)
        {
            await _deckListCollection.ReplaceOneAsync(x => x.Id == id, deck);
            return;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var filter = Builders<DeckList>.Filter.Eq(x => x.Id, id);
            await _deckListCollection.DeleteOneAsync(filter);
            return;
        }

        public async Task DeleteByTitleAsync(string title)
        {
            var filter = Builders<DeckList>.Filter.Eq(x => x.Title.ToLower(), title.ToLower());
            await _deckListCollection.DeleteOneAsync(filter);
            return;
        }

        public async Task<List<DeckList>> GetByUserIdAsync(int userId)
        {
            return await _deckListCollection.Find(x => x.UserId == userId).ToListAsync();
        }

        public async Task<bool> DeleteUserDeckAsync(int deckId, int userId)
        {
            // This filter is strict. Both must match exactly.
            var filter = Builders<DeckList>.Filter.And(
                Builders<DeckList>.Filter.Eq(x => x.Id, deckId),
                Builders<DeckList>.Filter.Eq(x => x.UserId, userId)
            );

            var result = await _deckListCollection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

    }
}
