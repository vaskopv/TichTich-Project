using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TichTich.Common;
using TichTich.Data;
using TichTich.Data.Common.Models;
using TichTich.Data.Common.Repositories;
using TichTich.Data.Models;

namespace TichTich.Web.Controllers
{
    public class RacersController : BaseController
    {
        private readonly ApplicationDbContext db;
        private readonly IDeletableEntityRepository<Race> racesRepository;

        public RacersController(ApplicationDbContext db, IDeletableEntityRepository<Race> racesRepository)
        {
            this.db = db;
            this.racesRepository = racesRepository;
        }

        [Authorize]
        public async Task<IActionResult> Participate(int raceId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var race = this.db.Races.Where(x => x.Id == raceId).FirstOrDefault();
            var racerRace = new RacerRace
            {
                Race = race,
                RaceId = race.Id,
                Racer = this.db.Users.Where(x => x.Id == userId).FirstOrDefault(),
                RacerId = userId,
            };

            await this.db.RacerRaces.AddAsync(racerRace);
            await this.db.SaveChangesAsync();

            return this.Redirect("~/races/byid/" + race.Id);
        }

        public IActionResult ShowMyRaces()
        {
            return this.View();
        }

    }
}
