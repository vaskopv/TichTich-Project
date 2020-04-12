namespace TichTich.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TichTich.Data.Models;

    public class DistanceSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Distances.Any())
            {
                return;
            }

            var distances = new List<double> { 5, 10, 21.1, 42.2, 50, 100, };

            foreach (var distance in distances)
            {
                RaceType type = RaceType.None;

                if (distance == 5)
                {
                    type = RaceType.FiveK;
                }
                else if (distance == 10)
                {
                    type = RaceType.TenK;
                }
                else if (distance == 21.1)
                {
                    type = RaceType.HalfMarahon;
                }
                else if (distance == 42.2)
                {
                    type = RaceType.Marathon;
                }
                else if (distance > 42.2)
                {
                    type = RaceType.UltraMarathon;
                }

                await dbContext.Distances.AddAsync(new Distance
                {
                    Length = distance,
                    Type = type,
                });
            }
        }
    }
}
