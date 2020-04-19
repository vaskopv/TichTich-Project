using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TichTich.Common;
using TichTich.Data;
using TichTich.Data.Models;

namespace TichTich.Web.Controllers
{
    public class RacersController : BaseController
    {
        private readonly ApplicationDbContext db;

        public RacersController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Participate()
        {
            return this.View();
        }

        public IActionResult ShowMyRaces()
        {
            return this.View();
        }

    }
}
