using AutoMapper;
using ECommerce.Core.Abstractions.RepostoriesInterfaces;
using ECommerce.Core.Abstractions.ServicesInterfaces;
using ECommerce.Core.Entities;
using ECommerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class ProductsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var productEntity = await _unitOfWork.ProductsRepository.GetByIdAsync(id);
            if (productEntity is null) throw new ArgumentException("Такого товара не существует");

            var product = _mapper.Map<Product>(productEntity);
            return product;
        }

        public async Task AddProduct(string name, string description,decimal price, int stockQuantity, string categoryName)
        {
            var categoryEntity = await _unitOfWork.CategoriesRepository.GetByNameAsync(categoryName);

            if (categoryEntity is null) throw new ArgumentException("Такой категории не существует");

            var categoryId = categoryEntity.CategoryId;

            var product = new Product(categoryId, name, description, price, stockQuantity);
            var productEntity = _mapper.Map<ProductEntity>(product);

            await _unitOfWork.ProductsRepository.AddAsync(productEntity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateProductInfo(int productId, string categoryName, string name, string description, decimal price)
        {
            var productEntity = await _unitOfWork.ProductsRepository.GetByIdAsync(productId);

            if (productEntity is null) throw new ArgumentException("Такого товара не существует");

            var categoryEntity = await _unitOfWork.CategoriesRepository.GetByNameAsync(categoryName);

            if (categoryEntity is null) throw new ArgumentException("Такой категории не существует");

            var categoryId = categoryEntity.CategoryId;

            await _unitOfWork.ProductsRepository.UpdateInfoAsync(productId, name, description, price);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateProductQuantity(int productId, int Quantity)
        {
            var productEntity = await _unitOfWork.ProductsRepository.GetByIdAsync(productId);

            if (productEntity is null) throw new ArgumentException("Такого товара не существует");

            await _unitOfWork.ProductsRepository.UpdateQuantityAsync(productId, Quantity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteProduct(int productId)
        {
            var productEntity = await _unitOfWork.ProductsRepository.GetByIdAsync(productId);

            if (productEntity is null) throw new ArgumentException("Такого товара не существует");

            await _unitOfWork.ProductsRepository.DeleteAsync(productId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<Product>> GetProductsByCategory(string categoryName)
        {
            var categoryEntity = await _unitOfWork.CategoriesRepository.GetByNameAsync(categoryName);
            var categotyId = categoryEntity.CategoryId;

            var productsEntities = await _unitOfWork.ProductsRepository.GetAllByCategoryAsync(categotyId);
            var products = _mapper.Map<List<Product>>(productsEntities);

            return products;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var productEntities = await _unitOfWork.ProductsRepository.GetAllAsync();

            var products = _mapper.Map<List<Product>>(productEntities);

            return products;
        }

    }
}
