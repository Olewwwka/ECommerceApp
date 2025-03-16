using AutoMapper;
using ECommerce.Core.Abstractions.RepostoriesInterfaces;
using ECommerce.Core.Abstractions.ServicesInterfaces;
using ECommerce.Core.Models;
using System.Net;

namespace ECommerce.Application.Services
{
    public class UserCartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        public UserCartService(IUnitOfWork unitOfWork, IMapper imapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = imapper;
            _currentUserService = currentUserService;
        }
        public async Task RemoveProductFromCart(int productId)
        {
            var userId = _currentUserService.UserId;
            if (userId == null) throw new UnauthorizedAccessException();

            var productEntity = await _unitOfWork.ProductsRepository.GetByIdAsync(productId);
            if (productEntity is null) throw new ArgumentNullException("Товар не найден");

            var quantity = await _unitOfWork.ShoppingCartsRepository.RemoveItemFromCartAsync(userId.Value, productId);
            await _unitOfWork.ProductsRepository.ChangeCountOfProductAsync(productId, -quantity);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateQuantityOfProduct(int productId, int quantity)
        {
            var userId = _currentUserService.UserId;
            if (userId == null) throw new UnauthorizedAccessException();

            await _unitOfWork.ShoppingCartsRepository.UpdateItemQuantityAsync(userId.Value, productId, quantity);
            await _unitOfWork.ProductsRepository.ChangeCountOfProductAsync(productId, quantity);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task AddProductToCart(int productId, int quantity)
        {
            var productEntity = await _unitOfWork.ProductsRepository.GetByIdAsync(productId);
            if (productEntity is null) throw new ArgumentNullException("Товар не найден");

            var product = _mapper.Map<Product>(productEntity);
            if (product.Quantity < quantity) throw new ArgumentException("Некорректное число товара");

            var userId = _currentUserService.UserId;
            if (userId == null) throw new UnauthorizedAccessException();

            await _unitOfWork.ShoppingCartsRepository.AddItemToCartAsync(userId.Value, productId, quantity);
            await _unitOfWork.ProductsRepository.ChangeCountOfProductAsync(productId, quantity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<Product>> GetCartProducts()
        {
            var userId = _currentUserService.UserId;
            if (userId == null) throw new UnauthorizedAccessException();

            var productEntities = await _unitOfWork.ShoppingCartsRepository.GetItemsInCartAsync(userId.Value);
            var products = _mapper.Map<List<Product>>(productEntities);

            return products;
        }
    }
}
