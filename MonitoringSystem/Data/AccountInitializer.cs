using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MonitoringSystem.Data
{
    public class AccountInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext _context, UserManager<IdentityUser> _userManager)
        {
            _context.Database.EnsureCreated();

            if (!_context.Users.Any())
            {

                var user = new IdentityUser()
                {
                    Email = "a@a.com",
                    UserName = "a@a.com",
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