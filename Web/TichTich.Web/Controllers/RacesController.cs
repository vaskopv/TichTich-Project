namespace TichTich.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TichTich.Data;
    using TichTich.Data.Models;
    using TichTich.Services.Data;
    using TichTich.Web.ViewModels.Races;

    public class RacesController : Controller

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
            var postId = await this.racesService.CreateAsync(input.Name, input.Description, user.Id, input.TerrainType);

            return this.RedirectToAction("ById", new { id = postId });
        }
    }
}
