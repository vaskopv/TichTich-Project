using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TichTich.Web.ViewModels.Results
{
    public class ResultsViewModel
    {
        public string RacerId { get; set; }

        public int RaceId { get; set; }

        public string RacerName { get; set; }

        public string FinishTime { get; set; }
    }
}
