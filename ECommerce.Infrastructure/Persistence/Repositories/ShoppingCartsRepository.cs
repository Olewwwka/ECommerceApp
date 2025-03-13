using ECommerce.Core.Abstractions.RepostoriesInterfaces;
using ECommerce.Core.Entities;
using ECommerce.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Persistence.Repositories
{
    public class ShoppingCartsRepository : IShoppingCartsRepository
    {
        private readonly ECommerceDbContext _context;
        public ShoppingCartsRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task AddItemToCartAsync(int userId, int productId, int quantity)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(cart => cart.Products)
                .FirstOrDefaultAsync(cart => cart.UserId == userId);

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCartEntity(userId);
                await _context.ShoppingCarts.AddAsync(shoppingCart);
            }

            var existingItem = shoppingCart.Products.FirstOrDefault(item => item.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var newItem = new ShoppingCartProductEntity
                {
                    ProductId = productId,
                    Quantity = quantity
                };
                shoppingCart.Products.Add(newItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateItemQuantityAsync(int userId, int productId, int quantity)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(cart => cart.Products)
                .FirstOrDefaultAsync(cart => cart.UserId == userId);

            if (shoppingCart == null)
            {
                throw new Exception("Shopping cart not found.");
            }

            var item = shoppingCart.Products.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Item not found in cart.");
            }
        }

        public async Task RemoveItemFromCartAsync(int userId, int productId)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(cart => cart.Products)
                .FirstOrDefaultAsync(cart => cart.UserId == userId);

            if (shoppingCart == null)
            {
                throw new Exception("Shopping cart not found.");
            }

            var item = shoppingCart.Products.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                shoppingCart.Products.Remove(item);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Item not found in cart.");
            }
        }

        public async Task ClearCartAsync(int userId)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(cart => cart.Products)
                .FirstOrDefaultAsync(cart => cart.UserId == userId);

            if (shoppingCart == null)
            {
                throw new Exception("Shopping cart not found.");
            }

            shoppingCart.Products.Clear();
            await _context.SaveChangesAsync();
        }

        public async Task<List<ShoppingCartProductEntity>> GetItemsInCartAsync(int userId)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(cart => cart.Products)
                .FirstOrDefaultAsync(cart => cart.UserId == userId);

            if (shoppingCart == null)
            {
                return new List<ShoppingCartProductEntity>();
            }

            return shoppingCart.Products.ToList();
        }


    }
}
