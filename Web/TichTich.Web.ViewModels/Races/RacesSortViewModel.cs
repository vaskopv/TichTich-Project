using System;
using System.Collections.Generic;
using System.Text;

namespace TichTich.Web.ViewModels.Races
{
    public class RacesSortViewModel
    {
        public string CurrentSort { get; set; }

        public string NameSortPattern { get; set; }

        public string DistanceSortPattern { get; set; }

        public string ParticipantsSortPattern { get; set; }

        public string CurrentFilter { get; set; }
    }
}
