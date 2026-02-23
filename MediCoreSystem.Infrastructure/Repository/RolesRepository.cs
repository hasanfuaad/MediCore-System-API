using MediCoreSystem.Domain.Entites;
using MediCoreSystem.Domain.IRepository;
using MediCoreSystem.Infrastructure.Data;
using Infrastructure.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCoreSystem.Infrastructure.Repository
{
   
    public class RolesRepository(AppDbContext productDbContext) : BaseRepository<Roles>(productDbContext), IRolesRepository
    {

    }
}
