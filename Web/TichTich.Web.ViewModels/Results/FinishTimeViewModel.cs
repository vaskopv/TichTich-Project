using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TichTich.Web.ViewModels.Results
{
    public class FinishTimeViewModel
    {
        public string RaceTime { get; set; }

        public int RaceId { get; set; }

        public string RacerId { get; set; }
    }
}
