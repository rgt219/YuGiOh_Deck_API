using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using YuGiOhDeckApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Org.BouncyCastle.Crypto.Generators;

namespace YuGiOhDeckApi.Data
{
    public class UserService
    {
        private readonly IMongoCollection<User> _userCollection;

        private string HashPassword(string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));

            // Convert password to the byte[] expected by the available BCrypt API
            var pwdChars = password.ToCharArray();
            byte[] passwordBytes = BCrypt.PasswordToByteArray(pwdChars);

            // Generate a 16-byte salt (bcrypt typically uses 128-bit salt)
            byte[] salt = new byte[16];
            RandomNumberGenerator.Fill(salt);

            // Use a reasonable cost factor (e.g., 12). Adjust as needed.
            int cost = 12;

            // Generate hashed bytes using the available BCrypt.Generate API
            byte[] hashedBytes = BCrypt.Generate(passwordBytes, salt, cost);

            // Store as Base64 string for persistence
            return Convert.ToBase64String(hashedBytes);
        }

        public UserService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _userCollection = database.GetCollection<User>(mongoDBSettings.Value.CollectionName);
        }

        public async Task<User> CreateAsync(User user)
        {
            // Hash the password using the implemented hashing logic
            user.PasswordHash = HashPassword(user.PasswordHash);
            await _userCollection.InsertOneAsync(user);
            return user;
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _userCollection.Find(new BsonDocument()).ToListAsync();
        }
    }
}
