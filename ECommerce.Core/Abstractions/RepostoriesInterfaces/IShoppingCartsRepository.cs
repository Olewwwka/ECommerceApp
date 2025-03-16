using ECommerce.Core.Entities;

namespace ECommerce.Core.Abstractions.RepostoriesInterfaces
{
    public interface IShoppingCartsRepository
    {
        Task AddItemToCartAsync(int userId, int productId, int quantity);
        Task ClearCartAsync(int userId);
        Task<List<ShoppingCartProductEntity>> GetItemsInCartAsync(int userId);
        Task<int> RemoveItemFromCartAsync(int userId, int productId);
        Task UpdateItemQuantityAsync(int userId, int productId, int quantity);
    }
}