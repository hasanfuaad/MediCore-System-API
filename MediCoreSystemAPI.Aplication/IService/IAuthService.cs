using Application.Common;
using Application.IService;
using MediCoreSystem.Aplication.DTOs;
using MediCoreSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCoreSystem.Aplication.IService
{
    public interface IAuthService
    {
        Task<Results<string>> LoginAsync(LoginDTO dto); 
        Task<Results<bool>> ChangePasswordAsync(int accountId, ChangePasswordDTO dto);
        Task<Results<bool>> LogoutAsync(int userId); 
    }

}
