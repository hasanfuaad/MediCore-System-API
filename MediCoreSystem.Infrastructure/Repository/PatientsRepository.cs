using Infrastructure.Repository.BaseRepository;
using MediCoreSystem.Domain.Entities;
using MediCoreSystem.Domain.IRepository;
using MediCoreSystem.Infrastructure.Data;

namespace MediCoreSystem.Infrastructure.Repository
{

    public class PatientsRepository(AppDbContext productDbContext) : BaseRepository<Patients>(productDbContext), IPatientsRepository
    {
    }
}
