using AutoMapper;
using ECommerce.Core.Abstractions.RepostoriesInterfaces;
using ECommerce.Core.Entities;
using ECommerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ECommerce.Application.Services
{
    public class CategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddCategory(string name, string description)
        {
            var existingCategory = await _unitOfWork.CategoriesRepository.GetByNameAsync(name);

            if (existingCategory != null)
            {
                throw new InvalidOperationException("Категория с таким именем уже существует.");
            }

            var category = new Category(name, description);
            var categoryEntity = _mapper.Map<CategoryEntity>(category);

            await _unitOfWork.CategoriesRepository.AddAsync(categoryEntity);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateCategory(string name, string description)
        {
            var existingCategory = await _unitOfWork.CategoriesRepository.GetByNameAsync(name);

            if (existingCategory == null)
            {
                throw new InvalidOperationException("Категория с таким именем не существует.");
            }

            var categoryId = existingCategory.CategoryId;

            await _unitOfWork.CategoriesRepository.UpdateAsync(categoryId, name, description);
            await _unitOfWork.SaveChangesAsync();

        }
        public async Task DeleteCategory(string name)
        {
            var existingCategory = await _unitOfWork.CategoriesRepository.GetByNameAsync(name);

            if (existingCategory == null)
            {
                throw new InvalidOperationException("Категория с таким именем не существует.");
            }

            var categoryId = existingCategory.CategoryId;

            await _unitOfWork.CategoriesRepository.DeleteAsync(categoryId);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<List<Category>> GetAllCategories()
        {
            var categoryEntites = await _unitOfWork.CategoriesRepository.GetAllAsync();

            var categories = _mapper.Map<List<Category>>(categoryEntites);

            return categories;
        }
    }
}
