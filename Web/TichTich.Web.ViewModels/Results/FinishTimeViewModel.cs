using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TichTich.Web.ViewModels.Results
{
    public class FinishTimeViewModel
    {
        [Required]
        [RegularExpression("^(?:(?:([01]?\\d|2[0-3]):)?([0-5]?\\d):)?([0-5]?\\d)$", ErrorMessage = "Enter Valid Time")]
        [Display(Name = "Finish Time")]
        public string RaceTime { get; set; }
    }
}
