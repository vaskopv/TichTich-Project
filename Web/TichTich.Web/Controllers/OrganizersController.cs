using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TichTich.Data;
using TichTich.Data.Models;
using TichTich.Services.Data;
using TichTich.Web.ViewModels.Races;
using TichTich.Web.ViewModels.Results;

namespace TichTich.Web.Controllers
{
    public class OrganizersController : BaseController
    {
        private readonly IOrganizersService organizersService;

        public OrganizersController(IOrganizersService organizersService)
        {
            this.organizersService = organizersService;
        }

        public IActionResult Results(int raceId)
        {
            var viewModel = this.organizersService.Results(raceId);
            return this.View(viewModel);
        }

        public IActionResult EnterTime(string racerId, int raceId)
        {
            var model = new FinishTimeViewModel
            {
                RaceId = raceId,
                RaceTime = "00:00:00",
                RacerId = racerId,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EnterTime(FinishTimeViewModel finishTime)
        {
            await this.organizersService.EnterTimeAsync(finishTime);

            return this.Redirect("~/organizers/results?raceId=" + finishTime.RaceId);
        }

        public IActionResult RemoveParticipant()
        {
            return this.View();
        }
    }
}
