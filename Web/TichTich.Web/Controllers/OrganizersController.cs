using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TichTich.Web.Controllers
{
    public class OrganizersController : BaseController
    {
        public IActionResult EditResults()
        {
            return this.View();
        }

        public IActionResult RemoveParticipant()
        {
            return this.View();
        }
    }
}
