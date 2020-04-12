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

    public class RacesService : IRacesService
    {
        private readonly IDeletableEntityRepository<Race> racesRepository;

        public RacesService(IDeletableEntityRepository<Race> racesRepository)
        {
            this.racesRepository = racesRepository;
        }

        public async Task<int> CreateAsync(string name, string description, string orgnizerId, TerrainType terrainType)
        {
            var race = new Race
            {
                Name = name,
                Description = description,
                TerrainType = terrainType,
                OrganizerId = orgnizerId,
            };

            await this.racesRepository.AddAsync(race);
            await this.racesRepository.SaveChangesAsync();

            return race.Id;
        }

        public IEnumerable<Race> GetAllRaces()
        {
            throw new NotImplementedException();
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
