using cltxmomo.Models;
using Microsoft.AspNetCore.Identity;

namespace cltxmomo.Data
{
    public class SeedData
    {
        public interface IIdentityDataInitializer
        {
            Task SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager);
        }

        public class IdentityDataInitializer : IIdentityDataInitializer
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly IConfiguration _configuration;

            public IdentityDataInitializer(
                UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager,
                IConfiguration configuration)
            {
                _userManager = userManager;
                _roleManager = roleManager;
                _configuration = configuration;
            }

            public async Task SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
            {

                // Add roles          
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("Member"));

                // Add super admin user
                var superAdminEmail = _configuration["SuperAdminDefaultOption:Email"];
                var superAdminUserName = _configuration["SuperAdminDefaultOption:Username"];
                var superAdminPassword = _configuration["SuperAdminDefaultOption:Password"];
                var superAdminUser = new ApplicationUser
                {
                    Email = superAdminEmail,
                    UserName = superAdminUserName,

                };

                var result = await _userManager.CreateAsync(superAdminUser, superAdminPassword);



                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(superAdminUser, "Admin");

                }
            }
        }
    }
}