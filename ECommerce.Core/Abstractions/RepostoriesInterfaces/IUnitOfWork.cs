using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Abstractions.RepostoriesInterfaces
{
    public interface IUnitOfWork
    {
        IUsersRepository UsersRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        void Dispose();
        Task<int> SaveChangesAsync();
    }
}
