using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Abstractions.RepostoriesInterfaces
{
    public interface IRefreshTokenRepository
    {
        Task<string?> GetRefreshTokenAsync(int userId);
        Task<int?> GetUserIdByRefreshToken(string refreshToken);
        Task RemoveRefreshTokenAsync(int userId);
        Task SetRefreshTokenAsync(int userId, string refreshToken, TimeSpan expires);
    }
}
