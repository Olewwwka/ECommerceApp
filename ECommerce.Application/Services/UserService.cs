using AutoMapper;
using ECommerce.Core.Abstractions.RepostoriesInterfaces;
using ECommerce.Core.Abstractions.ServicesInterfaces;
using ECommerce.Core.Entities;
using ECommerce.Core.Enums;
using ECommerce.Core.Models;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Services
{
    public class UserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtProvider _jwtProvider;
        public UserService(IPasswordHasher passwordHasher,
            IUnitOfWork unitOfWOrk,
            IMapper mapper,
            IJwtProvider jwtProvider)
        {
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWOrk;
            _mapper = mapper;
            _jwtProvider = jwtProvider;
        }
        public async Task Register(string login, string email, string password, string firstname, string lastname)
        {
            if (await _unitOfWork.UsersRepository.UserExistsAsync(email, login))
            {
                throw new InvalidOperationException("User with this login or email already exists");
            }

            var hashedPassword = _passwordHasher.Generate(password);

            var user = User.Create(login, hashedPassword, email, firstname, lastname);

            var userEntity = _mapper.Map<UserEntity>(user);

            await _unitOfWork.UsersRepository.AddAsync(userEntity);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<(string jwtToken, string refreshToken)> Login(string email, string password)
        {
            var userEntity = await _unitOfWork.UsersRepository.GetByEmailAsync(email);

            var result = _passwordHasher.Verify(password, userEntity.PasswordHash);

            if (result == false)
            {
                throw new Exception("wrongpassword");
            }
            var token = _jwtProvider.GenerateToken(userEntity);
            var refreshToken = _jwtProvider.GenerateRefreshToken();
            await _unitOfWork.RefreshTokenRepository.SetRefreshTokenAsync(userEntity.UserId, refreshToken, TimeSpan.FromDays(2));

            return (token, refreshToken);
        }

        public async Task<List<User>> GetAllUsers()
        {
            var userEntities = await _unitOfWork.UsersRepository.GetAllAsync();

            var users = _mapper.Map<List<User>>(userEntities);

            return users;
        }

        public async Task<(string? newAccessToken, string? newRefreshToken)> ValidateAndRefreshToken(string refreshToken)
        {
            var userId = await _unitOfWork.RefreshTokenRepository.GetUserIdByRefreshToken(refreshToken);


            if (userId is -1)
            {
                return (null, null);
            }

            var user = await _unitOfWork.UsersRepository.GetByIdAsync((int)userId);

            var newAccessToken = _jwtProvider.GenerateToken(user);
            var newRefreshToken = _jwtProvider.GenerateRefreshToken();

            await _unitOfWork.RefreshTokenRepository.RemoveRefreshTokenAsync((int)userId);
            await _unitOfWork.RefreshTokenRepository.SetRefreshTokenAsync((int)userId, newRefreshToken, TimeSpan.FromDays(2));

            return (newAccessToken, newRefreshToken);
        }

        public Task<HashSet<Permission>> GetPermissionsAsync(int id)
        {
            return _unitOfWork.UsersRepository.GetUserPermissionsAsync(id);
        }
    }
}
