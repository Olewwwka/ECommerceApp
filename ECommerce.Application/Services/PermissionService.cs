using Core.Abstractions.ServicesInterfaces;
using ECommerce.Core.Abstractions.RepostoriesInterfaces;
using ECommerce.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<HashSet<Permission>> GetPermissionsAsync(int id)
        {
            return _unitOfWork.UsersRepository.GetUserPermissionsAsync(id);
        }
    }
}
