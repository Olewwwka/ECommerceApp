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
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ECommerceDbContext _context;
        public CategoriesRepository(ECommerceDbContext context)
        {
            _context = context;
        }
        public async Task<List<CategoryEntity>> GetAllAsync()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }
        public async Task<CategoryEntity> GetByNameAsync(string name)
        {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name);
        }
        public async Task<CategoryEntity> GetByIdAsync(int id)
        {
            return await _context.Categories.AsNoTracking()
                .FirstOrDefaultAsync(category => category.CategoryId == id);
        }
        public async Task AddAsync(CategoryEntity categoryEntity)
        {
            await _context.Categories.AddAsync(categoryEntity);
            await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateAsync(int id, string name, string description)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(x => x.CategoryId == id);

            if (category != null)
            {
                category.Name = name;
                category.Description = description;
                await _context.SaveChangesAsync();
                return category.CategoryId;
            }

            return -1;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(x => x.CategoryId == id);

            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return id;
            }

            return -1;
        }
    }
}
