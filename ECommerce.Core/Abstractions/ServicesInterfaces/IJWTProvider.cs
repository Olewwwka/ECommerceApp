using ECommerce.Core.Entities;
using ECommerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Abstractions.ServicesInterfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(UserEntity user);
        string GenerateRefreshToken();
    }
}
