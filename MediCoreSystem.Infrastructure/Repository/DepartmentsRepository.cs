using Infrastructure.Repository.BaseRepository;
using MediCoreSystem.Domain.Entities;
using MediCoreSystem.Domain.IRepository;
using MediCoreSystem.Infrastructure.Data;

namespace MediCoreSystem.Infrastructure.Repository
{

    public class DepartmentsRepository(AppDbContext productDbContext) : BaseRepository<Departments>(productDbContext), IDepartmentsRepository
    {
    }
}
