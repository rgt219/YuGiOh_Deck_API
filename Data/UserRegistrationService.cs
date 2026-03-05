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
    public class UserRegistrationService
    {
        private readonly IMongoCollection<UserRegistration> _users;

        public UserRegistrationService(IOptions<MongoDBUserSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _users = database.GetCollection<UserRegistration>(mongoDBSettings.Value.CollectionName);
        }

        public async Task<UserRegistration?> GetByEmailAsync(string email) =>
            await _users.Find(u => u.Email == email).FirstOrDefaultAsync();

        public async Task CreateAsync(UserRegistration newUser) =>
            await _users.InsertOneAsync(newUser);
    }
}
