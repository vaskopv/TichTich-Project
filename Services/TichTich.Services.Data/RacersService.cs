using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TichTich.Data.Common.Repositories;
using TichTich.Data.Models;

namespace TichTich.Services.Data
{
    public class RacersService : IRacersService
    {
        private readonly IDeletableEntityRepository<Race> racesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IRepository<RacerRace> racersRaceRepository;

        public RacersService(IDeletableEntityRepository<Race> racesRepository, IDeletableEntityRepository<ApplicationUser> usersRepository, IRepository<RacerRace> racersRaceRepository)
        {
            this.racesRepository = racesRepository;
            this.usersRepository = usersRepository;
            this.racersRaceRepository = racersRaceRepository;

        }

        public IRepository<RacerRace> RacerRacesRepository { get; }

        public async Task Participate(int raceId, string userId)
        {
            var race = this.racesRepository.All().FirstOrDefault(x => x.Id == raceId);

            var racerRace = new RacerRace
            {
                Race = race,
                RaceId = race.Id,
                Racer = this.usersRepository.All().FirstOrDefault(x => x.Id == userId),
                RacerId = userId,
            };

            await this.racersRaceRepository.AddAsync(racerRace);
            await this.racersRaceRepository.SaveChangesAsync();
        }
    }
}
