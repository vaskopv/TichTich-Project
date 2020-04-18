namespace TichTich.Data.Models
{
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using TichTich.Data.Common.Models;
using TichTich.Data.Models.Enums;

public class Race : BaseDeletableModel<int>
    {
        public Race()
        {
            this.Racers = new HashSet<RacerRace>();
            this.Results = new HashSet<Result>();
        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public double Distance { get; set; }

        public ApplicationUser Organizer { get; set; }

        public string OrganizerId { get; set; }

        public RaceType RaceType { get; set; }

        public TerrainType TerrainType { get; set; }

        public virtual ICollection<RacerRace> Racers { get; set; }

        public virtual ICollection<Result> Results { get; set; }

        public int CutoffTime { get; set; }
    }
}
