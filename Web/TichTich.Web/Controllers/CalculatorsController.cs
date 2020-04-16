using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TichTich.Web.Controllers
{
    public class CalculatorsController : BaseController
    {
        public IActionResult RacePredictor()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult RacePredictor(string hours, string minutes, string seconds)
        {
            var time = TimeSpan.Parse(hours + ":" + minutes + ":" + seconds);
            var result = time.TotalMinutes * Math.Pow(2, 1.15);
            this.ViewBag.Result = TimeSpan.FromMinutes(result).ToString("hh\\:mm\\:ss");

            return this.View();
        }
    }
}
