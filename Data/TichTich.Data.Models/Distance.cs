namespace TichTich.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TichTich.Data.Common.Models;

    public class Distance : BaseDeletableModel<int>
    {
        public Distance()
        {
            this.Races = new HashSet<DistanceRace>();
        }

        public RaceType Type { get; set; }

        [Required]
        public double Length { get; set; }

        public ICollection<DistanceRace> Races { get; set; }
    }
}
