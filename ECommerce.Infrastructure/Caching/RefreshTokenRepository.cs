using ECommerce.Core.Abstractions.RepostoriesInterfaces;
using ECommerce.Core.Entities;
using ECommerce.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            var oldToken = await _cache.StringGetAsync($"user:{userId}");

            if (!oldToken.IsNullOrEmpty)
            {
                await _cache.KeyDeleteAsync($"refreshToken:{oldToken}");
            }

            await _cache.StringSetAsync($"refreshToken:{refreshToken}", userId.ToString(), expires);

            await _cache.StringSetAsync($"user:{userId}", refreshToken, expires);
        }

        public async Task<string?> GetRefreshTokenAsync(int userId)
        {
            var token = await _cache.StringGetAsync($"user:{userId}");
            return token.HasValue ? token.ToString() : null;
        }
        public async Task RemoveRefreshTokenAsync(int userId)
        {
            var token = await _cache.StringGetAsync($"user:{userId}");
            if (!token.IsNullOrEmpty)
            {
                await _cache.KeyDeleteAsync($"refreshToken:{token}");
            }
            await _cache.KeyDeleteAsync($"user:{userId}");
        }
        public async Task<int?> GetUserIdByRefreshToken(string refreshToken)
        {
            var stringUserId = await _cache.StringGetAsync($"refreshToken:{refreshToken}");
            return int.TryParse(stringUserId, out var userId) ? userId : null;
        }
    }
}
