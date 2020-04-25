namespace TichTich.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TichTich.Common;
    using TichTich.Data;
    using TichTich.Data.Models;
    using TichTich.Services.Data;
    using TichTich.Web.ViewModels.Races;

    public class RacesController : BaseController
    {
        private readonly ApplicationDbContext db;
        private readonly IRacesService racesService;
        private readonly UserManager<ApplicationUser> userManager;

        public RacesController(ApplicationDbContext db, IRacesService racesService, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.racesService = racesService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.OrganizerRoleName)]
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var races = this.racesService.GetByOrganizerId(userId);

            return this.View(races);
        }

        public IActionResult ByType(string type)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = new ByTypeViewModel();
            var races = this.db.Races
                .Select(x => new TerrainTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Distance = x.Distance,
                    OrganizerName = x.Organizer.UserName,
                    TerrainType = x.TerrainType,
                    IsParticipating = this.db.RacerRaces.Any(r => r.RaceId == x.Id && r.RacerId == userId),
                })
                .AsEnumerable()
                .Where(r => r.TerrainType.ToString() == type)
                .ToList();

            viewModel.TerrainType = type;
            viewModel.Races = races;

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.OrganizerRoleName)]
        public IActionResult Create()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.OrganizerRoleName)]
        public async Task<IActionResult> Create(CreateRaceInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var raceId = await this.racesService.CreateAsync(input.Name, input.Distance, input.Description, user.Id, input.TerrainType);

            return this.RedirectToAction("ById", new { id = raceId });
        }

        [Authorize(Roles = GlobalConstants.OrganizerRoleName)]
        public IActionResult Edit(int id)
        {
            var viewModel = this.racesService.Edit(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.OrganizerRoleName)]
        public async Task<IActionResult> Edit(EditRaceInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var viewModel = await this.racesService.EditAsync(input);

            return this.RedirectToAction("ById", new { id = viewModel });
        }

        [Authorize(Roles = GlobalConstants.OrganizerRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.racesService.Delete(id);

            return this.RedirectToAction("Index", "Races");
        }

        [Authorize]
        public IActionResult ById(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = this.racesService.GetByRaceId(id, userId);

            return this.View(viewModel);
        }
    }
}
