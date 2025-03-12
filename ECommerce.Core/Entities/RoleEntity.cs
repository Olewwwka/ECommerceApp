using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities
{
    public class RoleEntity
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public ICollection<PermissionEntity> Permisions { get; set; } = [];
        public ICollection<UserEntity> Users { get; set; } = [];

    }
}
