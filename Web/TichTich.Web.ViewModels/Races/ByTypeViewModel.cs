namespace TichTich.Web.ViewModels.Races
{
    using System.Collections.Generic;

    public class ByTypeViewModel
    {
        public IEnumerable<TerrainTypeViewModel> Races { get; set; }

        public string TerrainType { get; set; }

    }
}
