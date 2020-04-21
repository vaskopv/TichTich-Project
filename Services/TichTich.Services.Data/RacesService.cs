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
        private readonly IRepository<RacerRace> racersRaceRepository;

        public RacesService(IDeletableEntityRepository<Race> racesRepository, UserManager<ApplicationUser> userManager, IDeletableEntityRepository<ApplicationUser> usersRepository, IRepository<RacerRace> racersRaceRepository)
        {
            this.racesRepository = racesRepository;
            this.userManager = userManager;
            this.usersRepository = usersRepository;
            this.racersRaceRepository = racersRaceRepository;
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

        public IEnumerable<ByIdViewModel> GetAllRaces(string sortOrder, string searchString, int? page, RacesSortViewModel sortModel, string userId)
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
            var races = new List<ByIdViewModel>();
            var allRaces = this.racesRepository.All().Where(x => x.OrganizerId == userId);

            if (!String.IsNullOrEmpty(searchString))
            {
                allRaces = allRaces.Where(s => s.Name.Contains(searchString));
            }

            foreach (var item in allRaces)
            {
                races.Add(this.GetById(item.Id));
            }


            switch (sortOrder)
            {
                case "name_desc":
                    races = races.OrderByDescending(r => r.Name).ToList();
                    break;
                case "Distance":
                    races = races.OrderBy(r => r.Distance).ToList();
                    break;
                case "dist_desc":
                    races = races.OrderByDescending(r => r.Distance).ToList();
                    break;
                case "Participants":
                    races = races.OrderBy(r => r.RacersCount).ToList();
                    break;
                case "part_desc":
                    races = races.OrderByDescending(r => r.RacersCount).ToList();
                    break;
                default:
                    races = races.OrderBy(r => r.Name).ToList();
                    break;
            }

            return races;
        }

        public ByIdViewModel GetById(int id)
        {
            var race = this.racesRepository.All().FirstOrDefault(x => x.Id == id);

            var count = this.racersRaceRepository.All().Where(x => x.RaceId == id).Count();

            var result = new ByIdViewModel
            {
                Id = race.Id,
                Name = race.Name,
                OrganizerName = this.usersRepository
                    .All()
                    .FirstOrDefault(x => x.Id == race.OrganizerId)
                    .UserName,
                Description = race.Description,
                Distance = race.Distance,
                RacersCount = count,
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
