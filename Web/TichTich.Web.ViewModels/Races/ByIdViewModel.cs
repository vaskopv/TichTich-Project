using System;
using System.Collections.Generic;
using System.Text;
using TichTich.Data.Models;

namespace TichTich.Web.ViewModels.Races
{
    public class ByIdViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string OrganizerName { get; set; }

        public double Distance { get; set; }

        public ICollection<Result> Results { get; set; }

        public ICollection<RacerRace> Racers { get; set; }
    }
}
