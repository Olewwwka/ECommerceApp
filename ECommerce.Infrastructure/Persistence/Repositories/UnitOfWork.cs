using ECommerce.Core.Abstractions.RepostoriesInterfaces;
using ECommerce.Infrastructure.Persistence.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ECommerceDbContext _context;
        public IUsersRepository UsersRepository { get; }
        public UnitOfWork(ECommerceDbContext context)
        {
            _context = context;
            UsersRepository = new UsersRepository(context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
