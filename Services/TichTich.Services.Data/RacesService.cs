namespace TichTich.Services.Data
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using TichTich.Data.Common.Repositories;
    using TichTich.Data.Models;
    using TichTich.Data.Models.Enums;
    using TichTich.Services.Mapping;
    using TichTich.Web.ViewModels.Races;

    public class RacesService : IRacesService
    {
        private readonly IDeletableEntityRepository<Race> racesRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public RacesService(IDeletableEntityRepository<Race> racesRepository, UserManager<ApplicationUser> userManager, IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.racesRepository = racesRepository;
            this.userManager = userManager;
            this.usersRepository = usersRepository;
        }

        public async Task<int> CreateAsync(string name, double distance, string description, string orgnizerId, TerrainType terrainType)
        {
            var race = new Race
            {
                Name = name,
                Distance = distance,
                Description = description,
                TerrainType = terrainType,
                OrganizerId = orgnizerId,
            };

            await this.racesRepository.AddAsync(race);
            await this.racesRepository.SaveChangesAsync();

            return race.Id;
        }

        public IEnumerable<Race> GetAllRaces(string sortOrder, string searchString, int? page, RacesSortViewModel sortModel, string userId)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = sortModel.CurrentFilter;
            }

            sortModel.CurrentFilter = searchString;
            var races = from r in this.racesRepository.All().Where(x => x.OrganizerId == userId)
                        select r;
            if (!String.IsNullOrEmpty(searchString))
            {
                races = races.Where(s => s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    races = races.OrderByDescending(r => r.Name);
                    break;
                case "Distance":
                    races = races.OrderBy(r => r.Distance);
                    break;
                case "dist_desc":
                    races = races.OrderByDescending(r => r.Distance);
                    break;
                case "Participants":
                    races = races.OrderBy(r => r.Racers.Count);
                    break;
                case "part_desc":
                    races = races.OrderByDescending(r => r.Racers.Count);
                    break;
                default:
                    races = races.OrderBy(r => r.Name);
                    break;
            }

            return races;
        }

        public ByIdViewModel GetById(int id)
        {
            var race = this.racesRepository.All().FirstOrDefault(x => x.Id == id);

            var result = new ByIdViewModel
            {
                Name = race.Name,
                OrganizerName = this.usersRepository
                    .All()
                    .FirstOrDefault(x => x.Id == race.OrganizerId)
                    .UserName,
                Description = race.Description,
                Distance = race.Distance,
                Racers = race.Racers,
                Results = race.Results,
            };

            return result;
        }

        public IEnumerable<Race> GetByTerrainType(string type)
        {
            Enum.TryParse(type, out TerrainType terrainType);

            var races = this.racesRepository.All().Where(x => x.TerrainType == terrainType).ToList();

            return races;
        }

        public T GetByType<T>(string type)
        {
            Enum.TryParse(type, out TerrainType terrainType);

            var races = this.racesRepository
                .All()
                .Where(x => x.TerrainType == terrainType)
                .To<T>()
                .FirstOrDefault();

            return races;
        }
    }
}
