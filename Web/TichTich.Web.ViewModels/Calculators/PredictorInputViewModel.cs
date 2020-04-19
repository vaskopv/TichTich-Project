using System.ComponentModel.DataAnnotations;

namespace TichTich.Web.Controllers
{
    public class PredictorInputViewModel
    {
        [Required]
        [Range(0, 4, ErrorMessage = "Please enter valid time.")]
        [Display(Name = "Hours")]
        public int Hours { get; set; }

        [Required]
        [Range(0, 60, ErrorMessage = "Please enter valid time.")]
        [Display(Name = "Minutes")]
        public int Minutes { get; set; }

        [Required]
        [Range(0, 60, ErrorMessage = "Please enter valid time.")]
        [Display(Name = "Seconds")]
        public int Seconds { get; set; }
    }
}