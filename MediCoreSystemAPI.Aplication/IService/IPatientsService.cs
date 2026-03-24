using Application.IService;
using MediCoreSystem.Aplication.DTOs;

namespace MediCoreSystem.Aplication.IService
{
    public interface IPatientsService : IBaseServices<PatientsDTO>
    {
        //Task<Results<int>> InsertUserAsync(UserDTO value, int accountIdFromToken);

    }

}
