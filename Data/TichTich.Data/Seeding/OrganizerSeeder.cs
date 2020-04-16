namespace PersonalStockTrader.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using TichTich.Common;
    using TichTich.Data;
    using TichTich.Data.Models;
    using TichTich.Data.Seeding;

    public class OrganizerSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (userManager.Users.Any())
            {
                return;
            }

            var organizer = new ApplicationUser
            {
                UserName = configuration["Organizer:UserName"],
                Email = configuration["Organizer:Email"],
                EmailConfirmed = true,
            };

            var administratorPassword = configuration["Organizer:Password"];

            var result = await userManager.CreateAsync(organizer, administratorPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(organizer, GlobalConstants.OrganizerRoleName);
            }
        }
    }
}