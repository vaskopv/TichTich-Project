namespace TichTich.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TichTich.Services.Data;

    public class RacersController : BaseController
    {
        private readonly IRacersService racersService;

        public RacersController(IRacersService racersService)
        {
            this.racersService = racersService;
        }

        [Authorize]
        public async Task<IActionResult> Participate(int raceId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.racersService.Participate(raceId, userId);

            return this.Redirect("~/races/byid/" + raceId);
        }
    }
}
