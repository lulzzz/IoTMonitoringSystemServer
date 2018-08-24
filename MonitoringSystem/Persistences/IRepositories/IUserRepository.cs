using Microsoft.AspNetCore.Identity;
using MonitoringSystem.Models;
using MonitoringSystem.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonitoringSystem.Persistences.IRepositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetUsers();
        Task<ApplicationUser> GetUserByEmail(string email);
        void RemoveUser(ApplicationUser applicationUser);

    }
}
