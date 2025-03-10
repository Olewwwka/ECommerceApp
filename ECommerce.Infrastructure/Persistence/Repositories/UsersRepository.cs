using ECommerce.Core.Abstractions.RepostoriesInterfaces;
using ECommerce.Core.Entities;
using ECommerce.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ECommerceDbContext _context;
        public UsersRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserEntity user)
        {
            var shoppingCart = new ShoppingCartEntity
            {
                User = user,
                Products = new List<ProductEntity>()
            };

            await _context.ShoppingCarts.AddAsync(shoppingCart);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<List<UserEntity>> GetAllAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<UserEntity> GetByEmailAsync(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);
            return userEntity;
        }

    }
}
