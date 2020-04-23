using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TichTich.Data.Models.Enums;

namespace TichTich.Web.ViewModels.Races
{
    public class EditRaceInputViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        [Display(Name = "Race Name")]
        public string Name { get; set; }

        [Required]
        [Range(0.1, 300.00, ErrorMessage = "The {0} must be in the range between {1} and {2}.")]
        [Display(Name = "Distance")]
        public double Distance { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public string OrganizerId { get; set; }

        [Required(ErrorMessage = "You should pick {0}")]
        [Display(Name = "Terrain Type")]
        public TerrainType TerrainType { get; set; }
    }
}
