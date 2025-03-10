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
            var hashedPassword = _passwordHasher.Generate(password);

            var user = User.Create(login, hashedPassword, email, firstname, lastname);

            var userEntity = _mapper.Map<UserEntity>(user);

            await _unitOfWork.UsersRepository.AddAsync(userEntity);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<(string jwtToken, string refreshToken)> Login(string email, string password)
        {
            var userEntity = await _unitOfWork.UsersRepository.GetByEmailAsync(email);

            var user = _mapper.Map<User>(userEntity);
            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                throw new Exception("wrongpassword");
            }
            var token = _jwtProvider.GenerateToken(user);
            var refreshToken = _jwtProvider.GenerateRefreshToken();
            await _unitOfWork.RefreshTokenRepository.SetRefreshTokenAsync(userEntity.UserId, refreshToken, TimeSpan.FromHours(2));

            return (token, refreshToken);
        }

        public async Task<List<User>> GetAllUsers()
        {
            var userEntities = await _unitOfWork.UsersRepository.GetAllAsync();

            var users = _mapper.Map<List<User>>(userEntities);

            return users;
        }

        private void SetTokens(string jwtToken, string refreshToken)
        {
            var cookieOptions = 
        }
    }
}
