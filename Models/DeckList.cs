

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace YuGiOhDeckApi.Models
{
    public class DeckList
    {
        [BsonElement("mainDeck")]
        public List<Card>? MainDeck { get; set; }

        [BsonElement("extraDeck")]
        public List<Card>? ExtraDeck { get; set; }

        [BsonElement("sideDeck")]
        public List<Card>? SideDeck { get; set; }

        public DeckList()
        {
            MainDeck = new List<Card>();
            ExtraDeck = new List<Card>();
            SideDeck = new List<Card>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int? Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("userId")]
        public int UserId { get; set; }
    }

    public enum DeckSection
    {
        Main,
        Extra,
        Side
    }
}


