using ECommerce.Core.Abstractions.RepostoriesInterfaces;
using ECommerce.Core.Entities;
using ECommerce.Core.Enums;
using ECommerce.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

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
            var roleEntity = await _context.Roles
                .SingleOrDefaultAsync(r => r.RoleId == (int)Role.Admin)
                ?? throw new InvalidOperationException();

            var userEntity = new UserEntity()
            {
                UserId = user.UserId,
                Email = user.Email,
                LastName = user.LastName,
                FirstName = user.FirstName,
                Login = user.Login,
                PasswordHash = user.PasswordHash,
                Roles = [roleEntity],
                ShoppingCart = new ShoppingCartEntity()
            };

            await _context.Users.AddAsync(userEntity);
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

        public async Task<UserEntity> GetByIdAsync(int id)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == id);
            return userEntity;
        }

        public async Task<HashSet<Permission>> GetUserPermissionsAsync(int userId)
        {
            var roles = await _context.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .ThenInclude(p => p.Permisions)
                .Where(u => u.UserId == userId)
                .Select(u => u.Roles)
                .ToArrayAsync();

            return roles
                .SelectMany(r => r)
                .SelectMany(r => r.Permisions)
                .Select(p => (Permission)p.Id)
                .ToHashSet();
        }

    }
}
