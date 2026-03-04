using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace YuGiOhDeckApi.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("userName")]
        [JsonPropertyName("userName")]
        public string? UserName { get; set; }

        [BsonElement("email")]
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [BsonElement("passwordHash")]
        [JsonPropertyName("passwordHash")]
        public string? PasswordHash { get; set; }

    }
}
