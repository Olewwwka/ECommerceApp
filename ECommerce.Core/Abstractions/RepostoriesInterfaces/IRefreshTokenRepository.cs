using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Abstractions.RepostoriesInterfaces
{
    public interface IRefreshTokenRepository
    {
        Task SetRefreshTokenAsync(int userId, string refreshToken, TimeSpan expires);
        Task<string?> GetRefreshTokenAsync(string userId);
        Task RemoveRefreshTokenAsync(string userId);

    }
}
