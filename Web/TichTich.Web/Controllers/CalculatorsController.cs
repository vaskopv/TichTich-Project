using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TichTich.Services.Data;

namespace TichTich.Web.Controllers
{
    public class CalculatorsController : BaseController
    {
        private readonly ICalculatorsService calculatorsService;

        public CalculatorsController(ICalculatorsService calculatorsService)
        {
            this.calculatorsService = calculatorsService;
        }

        public IActionResult RacePredictor()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult RacePredictor(PredictorInputViewModel input)
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
