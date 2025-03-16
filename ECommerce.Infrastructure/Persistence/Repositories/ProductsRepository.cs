using ECommerce.Core.Abstractions.RepostoriesInterfaces;
using ECommerce.Core.Entities;
using ECommerce.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Persistence.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ECommerceDbContext _context;
        public ProductsRepository(ECommerceDbContext context)
        {
            _context = context;
        }
        public async Task ChangeCountOfProductAsync(int productId, int quantity)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {productId} not found.");
            }

            if (product.StockQuantity + quantity < 0)
            {
                throw new InvalidOperationException("Not enough stock available.");
            }

            product.StockQuantity -= quantity;
            await _context.SaveChangesAsync();
        }
        public async Task<List<ProductEntity>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Products
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<List<ProductEntity>> GetAllByCategoryAsync(int categoryId, int pageNumber, int pageSize)
        {
            return await _context.Products
                .Where(x => x.CategoryId == categoryId)
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<ProductEntity> GetByIdAsync(int id)
        {
            return await _context.Products.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProductId == id);
        }
        public async Task AddAsync(ProductEntity productEntity)
        {
            await _context.Products.AddAsync(productEntity);
        }

        public async Task<int> UpdateInfoAsync(int id, string name, string description, decimal price)
        {
            await _context.Products
                .Where(x => x.ProductId == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.Name, b => name)
                    .SetProperty(b => b.Description, b => description)
                    .SetProperty(b => b.Price, b => price)
                );
            return id;
        }

        public async Task<int> UpdateQuantityAsync(int id, int stockQuantity)
        {
            await _context.Products
                .Where(x => x.ProductId == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b=>b.StockQuantity, stockQuantity)
                );
            return id;
        }
        public async Task<int> DeleteAsync(int id)
        {
            await _context.Products
                .Where(x => x.ProductId == id)
                .ExecuteDeleteAsync();
            return id;
        }
    }
}
