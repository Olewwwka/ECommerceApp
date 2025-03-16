using ECommerce.Core.Entities;
using ECommerce.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Abstractions.RepostoriesInterfaces
{
    public interface IUsersRepository
    {
        Task<List<UserEntity>> GetAllAsync();
        Task AddAsync(UserEntity user);
        Task<UserEntity> GetByEmailAsync(string email);
        Task<UserEntity> GetByIdAsync(int id);
        Task<HashSet<Permission>> GetUserPermissionsAsync(int userId);
    }
}
