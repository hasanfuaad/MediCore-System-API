using Application.IService;
using MediCoreSystem.Aplication.DTOs;

namespace MediCoreSystem.Aplication.IService
{
    public interface IDoctorsService : IBaseServices<DoctorsDTO>
    {
        //Task<Results<int>> InsertUserAsync(UserDTO value, int accountIdFromToken);

    }

}
