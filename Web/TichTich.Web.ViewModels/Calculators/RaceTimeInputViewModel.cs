using System.ComponentModel.DataAnnotations;

namespace TichTich.Web.Controllers.Calculators
{
    public class RaceTimeInputViewModel
    {
        public int Hours { get; set; }

        public int Minutes { get; set; }

        public int Seconds { get; set; }
    }
}