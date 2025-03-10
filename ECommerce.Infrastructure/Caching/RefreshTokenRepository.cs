using ECommerce.Core.Abstractions.RepostoriesInterfaces;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Caching
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IDatabase _cache;

        public RefreshTokenRepository(IConfiguration configuration)
        {
            var redisConfig = configuration.GetSection("Redis");
            var host = redisConfig["Host"];
            var port = redisConfig["Port"];
            var password = redisConfig["Password"];

            var options = ConfigurationOptions.Parse($"{host}:{port}");
            options.Password = password;

            var redis = ConnectionMultiplexer.Connect(options);
            _cache = redis.GetDatabase();
        }

        public async Task SetRefreshTokenAsync(int userId, string refreshToken, TimeSpan expires)
        {
            await _cache.StringSetAsync($"refreshToken:{userId}", refreshToken, expires);
        }

        public async Task<string?> GetRefreshTokenAsync(string userId)
        {
            var token = await _cache.StringGetAsync($"refreshToken:{userId}");
            return token.HasValue ? token.ToString() : null;
        }

        public async Task RemoveRefreshTokenAsync(string userId)
        {
            await _cache.KeyDeleteAsync($"refreshToken:{userId}");
        }
    }
}
