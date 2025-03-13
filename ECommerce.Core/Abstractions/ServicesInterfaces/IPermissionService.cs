using ECommerce.Core.Enums;

namespace Core.Abstractions.ServicesInterfaces
{
    public interface IPermissionService
    {
        Task<HashSet<Permission>> GetPermissionsAsync(int id);
    }
}