namespace TichTich.Web.ViewModels.Races
{
    using System.Collections.Generic;
    using TichTich.Data.Models.Enums;

    public class ByTypeViewModel
    {
        public IEnumerable<TerrainTypeViewModel> Races { get; set; }

        public string TerrainType { get; set; }
    }
}
