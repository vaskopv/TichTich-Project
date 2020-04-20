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
            var postId = await this.racesService.CreateAsync(input.Name, input.Distance, input.Description, user.Id, input.TerrainType);

            return this.RedirectToAction("ById", new { id = postId });
        }

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            this.ViewBag.CurrentSort = sortOrder;
            this.ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : string.Empty;
            this.ViewBag.DistanceSortParm = sortOrder == "Distance" ? "dist_desc" : "Distance";
            this.ViewBag.ParticipantsSortParm = sortOrder == "Participants" ? "part_asc" : "Participants";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            this.ViewBag.CurrentFilter = searchString;
            var races = from r in this.db.Races.Where(x => x.OrganizerId == userId)
                         select r;
            if (!String.IsNullOrEmpty(searchString))
            {
                races = races.Where(s => s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    races = races.OrderByDescending(r => r.Name);
                    break;
                case "Distance":
                    races = races.OrderBy(r => r.Distance);
                    break;
                case "dist_desc":
                    races = races.OrderByDescending(r => r.Distance);
                    break;
                case "Participants":
                    races = races.OrderBy(r => r.Racers.Count);
                    break;
                case "part_desc":
                    races = races.OrderByDescending(r => r.Racers.Count);
                    break;
                default:
                    races = races.OrderBy(r => r.Name);
                    break;
            }

            int pageSize = 5;
            int pageNumber = page ?? 1;
            return this.View(races.ToPagedList(pageNumber, pageSize));
        }
    }
}
