using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TichTich.Data.Common.Repositories;
using TichTich.Data.Models;
using TichTich.Web.ViewModels.Results;

namespace TichTich.Services.Data
{
    public class OrganizersService : IOrganizersService
    {
        private readonly IDeletableEntityRepository<Race> racesRepository;
        private readonly IRepository<RacerRace> racerRacesRepository;
        private readonly IDeletableEntityRepository<Result> resultsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public OrganizersService(IDeletableEntityRepository<Race> racesRepository, IRepository<RacerRace> racerRacesRepository, IDeletableEntityRepository<Result> resultsRepository, IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.racesRepository = racesRepository;
            this.racerRacesRepository = racerRacesRepository;
            this.resultsRepository = resultsRepository;
            this.usersRepository = usersRepository;
        }

        public ICollection<ResultsViewModel> Results(int id)
        {
            var race = this.racesRepository.All().FirstOrDefault(x => x.Id == id);
            var racers = this.racerRacesRepository.All().Where(x => x.RaceId == id).ToList();

            var results = new List<ResultsViewModel>();

            foreach (var item in racers)
            {
                var finishtime = this.resultsRepository
                    .All()
                    .Where(x => x.UserId == item.RacerId && x.RaceId == race.Id)
                    .Select(x => x.FinishTime)
                    .FirstOrDefault();

                var outputTime = string.Empty;
                if (finishtime != null)
                {
                    outputTime = finishtime.ToString("hh\\:mm\\:ss");
                }

                results.Add(new ResultsViewModel
                {
                    RaceId = id,
                    RacerId = item.RacerId,
                    RacerName = this.usersRepository.All().FirstOrDefault(x => x.Id == item.RacerId).UserName,
                    FinishTime = outputTime,
                });
            }
            return results;
        }

        public async Task EnterTimeAsync(FinishTimeViewModel finishTime)
        {
            var race = this.racesRepository.All().FirstOrDefault(x => x.Id == finishTime.RaceId);

            var resultInput = new Result
            {
                FinishTime = TimeSpan.ParseExact(finishTime.RaceTime.ToString(), "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                RaceId = finishTime.RaceId,
                UserId = finishTime.RacerId,
            };

            race.Results.Add(resultInput);

            this.racesRepository.Update(race);
            await this.racesRepository.SaveChangesAsync();
        }
    }
}
