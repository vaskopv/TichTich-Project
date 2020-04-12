using System;
using System.Collections.Generic;
using System.Text;
using TichTich.Data.Models.Enums;

namespace TichTich.Web.ViewModels.Races
{
    public class CreateRaceInputModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string OrganizerId { get; set; }

        public TerrainType TerrainType { get; set; }
    }
}
