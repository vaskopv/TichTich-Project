namespace TichTich.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TichTich.Data.Models;

    public class RaceSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Races.Any())
            {
                return;
            }

            var races = new List<string> { "Sofia Marathon", "Plovdiv Marathon", "Tryavna Ultra", "Vitosha 100"};

            foreach (var race in races)
            {
                var rand = new Random();
                List<int> result = Enumerable.Range(0, rand.Next(100))
                .Select(x => rand.Next(10))
                .ToList();
                await dbContext.Races.AddAsync(new Race
                {
                    Name = race,
                    Description = race,
                    Organizer = dbContext.Users.FirstOrDefault(),
                });
            }
        }
    }
}
