using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TichTich.Data;
using TichTich.Data.Models;
using TichTich.Web.ViewModels.Races;
using TichTich.Web.ViewModels.Results;

namespace TichTich.Web.Controllers
{
    public class OrganizersController : BaseController
    {
        private readonly ApplicationDbContext db;

        public OrganizersController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult ResultsInput(int raceId)
        {
            var race = this.db.Races.Where(x => x.Id == raceId).FirstOrDefault();
            var racers = this.db.RacerRaces.Where(x => x.RaceId == raceId).ToList();

            var viewModel = new List<ResultsViewModel>();

            foreach (var item in racers)
            {
                viewModel.Add(new ResultsViewModel
                {
                    RacerName = this.db.Users.Where(x => x.Id == item.RacerId).FirstOrDefault().UserName,
                    FinishTime = string.Empty,
                });
            }

            return this.View(viewModel);

        }

        [HttpPost]
        public IActionResult ResultsInput(ICollection<Result> results)
        {
            return this.View();
        }

        public IActionResult RemoveParticipant()
        {
            return this.View();
        }
    }
}
