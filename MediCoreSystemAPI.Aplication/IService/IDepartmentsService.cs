using Application.IService;
using MediCoreSystem.Aplication.DTOs;

namespace MediCoreSystem.Aplication.IService
{
    public interface IDepartmentsService : IBaseServices<DepartmentsDTO>
    {
        //Task<Results<int>> InsertUserAsync(UserDTO value, int accountIdFromToken);

    }

}
