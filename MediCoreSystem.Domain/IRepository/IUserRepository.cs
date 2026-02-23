using MediCoreSystem.Domain.Entites;
using MediCoreSystem.Domain.IRepository.IBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCoreSystem.Domain.IRepository
{
    public interface IUserRepository : IBaseRepository<Users>
    {
        Task<Users?> GetByEmailAsync(string email);

    }
}
