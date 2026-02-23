using MediCoreSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCoreSystem.Aplication.IService
{
    public interface IJwtService
    {
        string GenerateToken(int accountId, int? userId, int roleId, bool isAccountOwner);
    }

}
