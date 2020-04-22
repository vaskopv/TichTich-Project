using System.Collections.Generic;
using TichTich.Data.Models;

namespace TichTich.Web.ViewModels.Races
{
    public class ByIdViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string OrganizerName { get; set; }

        public double Distance { get; set; }

        public int RacersCount { get; set; }

        public ICollection<Result> Results { get; set; }
    }
}
