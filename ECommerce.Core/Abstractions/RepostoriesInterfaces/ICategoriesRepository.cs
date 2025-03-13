using ECommerce.Core.Entities;

namespace ECommerce.Core.Abstractions.RepostoriesInterfaces
{
    public interface ICategoriesRepository
    {
        Task AddAsync(CategoryEntity categoryEntity);
        Task<int> DeleteAsync(int id);
        Task<List<CategoryEntity>> GetAllAsync();
        Task<CategoryEntity> GetByIdAsync(int id);
        Task<int> UpdateAsync(int id, string name, string description);
        Task<CategoryEntity> GetByNameAsync(string name);
    }
}