using Application.Common;
using Application.DTOs;
using Application.IService;
using MediCoreSystem.Aplication.DTOs;
using MediCoreSystem.Aplication.IService;
using MediCoreSystem.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediCoreSystem.Aplication.IService
{
    public interface IUserService : IBaseServices<UserDTO>
    {
        //Task<Results<int>> InsertUserAsync(UserDTO value, int accountIdFromToken);

    }

}
