using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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

        public IActionResult Results(int raceId)
        {
            var race = this.db.Races.Where(x => x.Id == raceId).FirstOrDefault();
            var racers = this.db.RacerRaces.Where(x => x.RaceId == raceId).ToList();

            var viewModel = new List<ResultsViewModel>();

            foreach (var item in racers)
            {
                var finishtime = race.Results.Where(x => x.UserId == item.RacerId).Select(x => x.FinishTime).FirstOrDefault();
                var outputTime = string.Empty;

                if (finishtime != null)
                {
                    outputTime = finishtime.ToString("hh\\:mm\\:ss");
                }

                viewModel.Add(new ResultsViewModel
                {
                    RaceId = raceId,
                    RacerId = item.RacerId,
                    RacerName = this.db.Users.Where(x => x.Id == item.RacerId).FirstOrDefault().UserName,
                    FinishTime = outputTime,
                });
            }

            return this.View(viewModel);
        }

        public IActionResult EnterTime(string racerId, int raceId, FinishTimeViewModel finishTime)
        {
            var race = this.db.Races.Where(x => x.Id == raceId).FirstOrDefault();

            var resultInput = new Result
            {
                FinishTime = TimeSpan.ParseExact(finishTime.ToString(), "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                RaceId = raceId,
                UserId = racerId,
            };

            race.Results.Add(resultInput);
            this.db.SaveChanges();

            return this.Redirect("~/organizers/results?raceId=" + raceId);
        }

        //[HttpPost]
        //public IActionResult EnterTime(string racerId, int raceId, FinishTimeViewModel finishTime)
        //{
        //    var race = this.db.Races.Where(x => x.Id == raceId).FirstOrDefault();

        //    var resultInput = new Result
        //    {
        //        FinishTime = TimeSpan.ParseExact(finishTime.ToString(), "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
        //        RaceId = raceId,
        //        UserId = racerId,
        //    };

        //    race.Results.Add(resultInput);
        //    this.db.SaveChanges();

        //    return this.Redirect("~/organizers/results?raceId=" + raceId);
        //}

        public IActionResult RemoveParticipant()
        {
            return this.View();
        }
    }
}
