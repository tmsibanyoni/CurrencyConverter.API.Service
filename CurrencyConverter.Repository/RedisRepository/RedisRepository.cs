using CurrencyConverter.Repository.Interface;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace CurrencyConverter.Repository.RedisRepository
{
    public class RedisRepository(ILogger<RedisRepository> logger, string connectionString) : IRedisRepository
    {
        private readonly ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect(connectionString);
        private readonly ILogger<RedisRepository> logger = logger;

        public string GetValue(string key)
        {
            var db = _redis.GetDatabase();
            return db.StringGet(key);
        }

        public void SetValue(string key, string value)
        {
            var db = _redis.GetDatabase();
            db.StringSet(key, value, TimeSpan.FromMinutes(15), true);
        }

        public bool KeyExists(string key)
        {
            var db = _redis.GetDatabase();
            return db.KeyExists(key);
        }

        public void DeleteKey(string key)
        {
            var db = _redis.GetDatabase();
            db.KeyDelete(key);
        }
    }
}
