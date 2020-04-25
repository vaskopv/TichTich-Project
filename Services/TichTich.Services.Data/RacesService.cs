namespace TichTich.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TichTich.Data.Common.Repositories;
    using TichTich.Data.Models;
    using TichTich.Data.Models.Enums;
    using TichTich.Services.Mapping;
    using TichTich.Web.ViewModels.Races;

    public class RacesService : IRacesService
    {
        private readonly IDeletableEntityRepository<Race> racesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IRepository<RacerRace> racersRaceRepository;

        public RacesService(IDeletableEntityRepository<Race> racesRepository, IDeletableEntityRepository<ApplicationUser> usersRepository, IRepository<RacerRace> racersRaceRepository)
        {
            this.racesRepository = racesRepository;
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

        public async Task<int> EditAsync(EditRaceInputViewModel input)
        {
            var race = this.racesRepository.All().FirstOrDefault(x => x.Id == input.Id);

            race.Id = input.Id;
            race.Description = input.Description;
            race.Distance = input.Distance;
            race.TerrainType = input.TerrainType;

            this.racesRepository.Update(race);
            await this.racesRepository.SaveChangesAsync();

            return race.Id;
        }

        public async Task Delete(int id)
        {
            var race = this.racesRepository.All().FirstOrDefault(x => x.Id == id);

            var racersRace = this.racersRaceRepository.All().Where(x => x.RaceId == race.Id).ToList();

            foreach (var item in racersRace)
            {
                this.racersRaceRepository.Delete(item);
            }

            this.racesRepository.Delete(race);
            await this.racesRepository.SaveChangesAsync();
        }

        public ICollection<ByIdViewModel> GetByOrganizerId(string id)
        {
            var races = this.racesRepository.All().Where(x => x.OrganizerId == id);

            var result = new List<ByIdViewModel>();

            foreach (var item in races)
            {
                var racersCount = this.racersRaceRepository.All().Where(x => x.RaceId == item.Id).Count();

                result.Add(new ByIdViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Distance = item.Distance,
                    RacersCount = racersCount,
                });
            }

            return result;
        }

        public ByIdViewModel GetByRaceId(int id, string userId)
        {
            var race = this.racesRepository.All().Where(x => x.Id == id).FirstOrDefault();

            var result = new ByIdViewModel
            {
                Id = race.Id,
                Name = race.Name,
                OrganizerName = this.usersRepository
                    .All()
                    .FirstOrDefault(x => x.Id == race.OrganizerId)
                    .UserName,
                Distance = race.Distance,
                Description = race.Description,
                Results = race.Results,
                IsParticipating = this.racersRaceRepository.All().Any(r => r.RaceId == id && r.RacerId == userId),
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

        public EditRaceInputViewModel Edit(int id)
        {
            var race = this.racesRepository.All().FirstOrDefault(x => x.Id == id);

            var editModel = new EditRaceInputViewModel
            {
                Id = race.Id,
                Name = race.Name,
                Description = race.Description,
                Distance = race.Distance,
                TerrainType = race.TerrainType,
            };

            return editModel;
        }
    }
}
