using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TichTich.Web.Controllers
{
    public class RacersController : BaseController
    {
        public IActionResult Participate()
        {
            return this.View();
        }
    }
}
