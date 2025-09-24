using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.User;
using DkGLobalPortfolio.WebApi.Services.IServices;
using DkGLobalPortfolio.WebApi.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class DbInitializerService : IDbInitializerService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DkGlobalPortfolioDbContext _db;

        public DbInitializerService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, DkGlobalPortfolioDbContext db)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync()
        {
            await ApplyMigrationsAsync();
            await SeedRolesAsync();
            await SeedAdminUserAsync();
        }

        private async Task ApplyMigrationsAsync()
        {
            if ((await _db.Database.GetPendingMigrationsAsync()).Any())
            {
                await _db.Database.MigrateAsync();
            }
            else
            {
                Console.WriteLine("ℹ️ No pending migrations.");
            }
        }
        private async Task SeedRolesAsync()
        {
            string[] roles = { RolesVariable.SUPERADMIN, RolesVariable.ADMIN};

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private async Task SeedAdminUserAsync()
        {
            const string adminEmail = "dkglobalfashion@gmail.com";
            const string adminPassword = "aDmin@00#";

            var existingAdmin = await _userManager.FindByEmailAsync(adminEmail);

            if (existingAdmin == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    PhoneNumber = "01970806028",
                    Password = adminPassword,
                };

                var result = await _userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, RolesVariable.ADMIN);
                }
                else
                {
                    throw new InvalidOperationException(
                        $"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}
