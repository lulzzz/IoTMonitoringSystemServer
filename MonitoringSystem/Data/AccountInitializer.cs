using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MonitoringSystem.Models;

namespace MonitoringSystem.Data
{
    public class AccountInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext _context, UserManager<ApplicationUser> _userManager)
        {
            _context.Database.EnsureCreated();

            if (!_context.Users.Any())
            {

                var user = new ApplicationUser()
                {
                    Email = "a@a.com",
                    UserName = "a@a.com",
                    FullName = "Admin",
                    PhoneNumber = "01693848597"
                };

                var result = await _userManager.CreateAsync(user, "abc@123");
                await _userManager.AddToRoleAsync(user, "Admin");

                if (result.Succeeded)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                }
            }
        }
    }
}