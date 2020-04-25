namespace TichTich.Web.Controllers
{
    using System;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TichTich.Services.Data;
    using TichTich.Web.Controllers.Calculators;

    public class CalculatorsController : BaseController
    {
        private readonly ICalculatorsService calculatorsService;

        public CalculatorsController(ICalculatorsService calculatorsService)
        {
            this.calculatorsService = calculatorsService;
        }

        [Authorize]
        public IActionResult RacePredictor()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult RacePredictor(RaceTimeInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var result = this.calculatorsService.Calculate(input);

            this.ViewBag.Result = TimeSpan.FromMinutes(result).ToString("hh\\:mm\\:ss");

            return this.View();
        }
    }
}
