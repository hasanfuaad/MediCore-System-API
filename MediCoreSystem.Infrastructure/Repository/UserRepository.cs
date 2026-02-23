using MediCoreSystem.Domain.Entites;
using MediCoreSystem.Domain.IRepository;
using MediCoreSystem.Infrastructure.Data;
using Infrastructure.Repository.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace MediCoreSystem.Infrastructure.Repository
{

    public class UserRepository : BaseRepository<Users>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Users?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
