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

    public class RacerSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (userManager.Users.Any())
            {
                return;
            }

            var racer = new ApplicationUser
            {
                UserName = configuration["Racer:UserName"],
                Email = configuration["Racer:Email"],
                EmailConfirmed = true,
            };

            var racerPassword = configuration["Runner:Password"];

            var result = await userManager.CreateAsync(racer, racerPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(racer, GlobalConstants.RacerRoleName);
            }
        }
    }
}