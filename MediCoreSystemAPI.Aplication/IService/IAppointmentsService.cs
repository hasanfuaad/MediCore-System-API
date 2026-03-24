using Application.IService;
using MediCoreSystem.Aplication.DTOs;

namespace MediCoreSystem.Aplication.IService
{
    public interface IAppointmentsService : IBaseServices<AppointmentsDTO>
    {
        //Task<Results<int>> InsertUserAsync(UserDTO value, int accountIdFromToken);

    }

}
