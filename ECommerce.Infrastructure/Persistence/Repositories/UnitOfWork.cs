using ECommerce.Core.Abstractions.RepostoriesInterfaces;
using ECommerce.Infrastructure.Caching;
using ECommerce.Infrastructure.Persistence.Configuration;
using Microsoft.Extensions.Configuration;
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
        public IRefreshTokenRepository RefreshTokenRepository { get; }
        public IProductsRepository ProductsRepository { get; }
        public ICategoriesRepository CategoriesRepository { get; }
        public IShoppingCartsRepository ShoppingCartsRepository { get; }

        public UnitOfWork(ECommerceDbContext context, IConfiguration configuration)
        {
            _context = context;
            UsersRepository = new UsersRepository(context);
            RefreshTokenRepository = new RefreshTokenRepository(configuration);
            ProductsRepository = new ProductsRepository(context);
            CategoriesRepository = new CategoriesRepository(context);
            ShoppingCartsRepository = new ShoppingCartsRepository(context);
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
