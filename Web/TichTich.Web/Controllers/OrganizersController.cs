namespace TichTich.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TichTich.Common;
    using TichTich.Services.Data;
    using TichTich.Web.ViewModels.Results;

    public class OrganizersController : BaseController
    {
        private readonly IOrganizersService organizersService;

        public OrganizersController(IOrganizersService organizersService)
        {
            this.organizersService = organizersService;
        }

        [Authorize(Roles = GlobalConstants.OrganizerRoleName)]
        public IActionResult Results(int raceId)
        {
            var viewModel = this.organizersService.Results(raceId);
            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.OrganizerRoleName)]
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
        [Authorize(Roles = GlobalConstants.OrganizerRoleName)]
        public async Task<IActionResult> EnterTime(FinishTimeViewModel finishTime)
        {

            if (!this.ModelState.IsValid)
            {
                return this.View(finishTime);
            }

            await this.organizersService.EnterTimeAsync(finishTime);
            return this.Redirect("~/organizers/results?raceId=" + finishTime.RaceId);
        }

        public IActionResult RemoveParticipant()
        {
            return this.View();
        }
    }
}
