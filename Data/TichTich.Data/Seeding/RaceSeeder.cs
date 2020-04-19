namespace TichTich.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TichTich.Common;
    using TichTich.Data.Models;

    public class RaceSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Races.Any())
            {
                return;
            }

            var races = new List<string> { "5kmRun", "10kmRun", "Sofia Half Marathon", "Plovdiv Marathon", "Tryavna Ultra", "Vitosha 100" };
            var distances = new List<double> { 5, 10, 21.1, 42.2, 50, 100 };

            for (int i = 0; i < races.Count(); i++)
            {
                var race = races[i];

                await dbContext.Races.AddAsync(new Race
                {
                    Name = race,
                    Description = race,
                    Distance = distances[i],
                    Organizer = dbContext.Users.Where(x => x.UserRole == GlobalConstants.OrganizerRoleName).FirstOrDefault(),
                });
            }
        }
    }
}
