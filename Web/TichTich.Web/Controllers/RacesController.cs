namespace TichTich.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TichTich.Data;
    using TichTich.Data.Models;
    using TichTich.Services.Data;
    using TichTich.Web.ViewModels.Races;
    using X.PagedList;

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

        public IActionResult ByType(string type)
        {
            var viewModel = new ByTypeViewModel();
            var races = this.db.Races
                .Select(x => new TerrainTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    OrganizerName = x.Organizer.UserName,
                    TerrainType = x.TerrainType,
                })
                .AsEnumerable()
                .Where(r => r.TerrainType.ToString() == type)
                .ToList();

            viewModel.TerrainType = type;
            viewModel.Races = races;

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            return this.View();
        }

        [HttpPost]
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

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var sortModel = new RacesSortViewModel
            {
                CurrentSort = sortOrder,
                NameSortPattern = string.IsNullOrEmpty(sortOrder) ? "name_desc" : string.Empty,
                DistanceSortPattern = sortOrder == "Distance" ? "dist_desc" : "Distance",
                ParticipantsSortPattern = sortOrder == "Participants" ? "part_asc" : "Participants",
            };

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            int pageSize = 5;
            int pageNumber = page ?? 1;

            var races = this.racesService.GetAllRaces(sortOrder, searchString, page, sortModel, userId).ToPagedList(pageNumber, pageSize);

            var viewModel = new CombinedViewModel
            {
                SortedModel = sortModel,
                Races = races,
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.racesService.GetById(id);

            return this.View(viewModel);
        }
    }
}
