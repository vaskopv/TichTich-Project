using System;
using System.Collections.Generic;
using System.Text;

namespace TichTich.Data.Models
{
    public class DistanceRace
    {
        public Distance Distance { get; set; }

        public int DistanceId { get; set; }

        public Race Race { get; set; }

        public int RaceId { get; set; }
    }
}
