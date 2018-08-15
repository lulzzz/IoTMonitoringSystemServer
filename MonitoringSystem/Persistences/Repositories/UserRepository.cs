using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MonitoringSystem.Data;
using MonitoringSystem.Models;
using MonitoringSystem.Persistences.IRepositories;

namespace MonitoringSystem.Persistences.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            return await context.ApplicationUser.ToListAsync();
        }
    }
}