using ECommerce.Core.Entities;

namespace ECommerce.Core.Abstractions.RepostoriesInterfaces
{
    public interface IProductsRepository
    {
        Task AddAsync(ProductEntity productEntity);
        Task<int> DeleteAsync(int id);
        Task<List<ProductEntity>> GetAllAsync(int pageNumber, int pageSize);
        Task<List<ProductEntity>> GetAllByCategoryAsync(int categoryId, int pageNumber, int pageSize);
        Task<ProductEntity> GetByIdAsync(int id);
        Task<int> UpdateInfoAsync(int id, string name, string description, decimal price);
        Task<int> UpdateQuantityAsync(int id, int stockQuantity);
    }
}