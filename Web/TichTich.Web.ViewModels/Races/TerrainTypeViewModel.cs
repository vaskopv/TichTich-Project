namespace TichTich.Web.ViewModels.Races
{
    using TichTich.Data.Models;
    using TichTich.Data.Models.Enums;
    using TichTich.Services.Mapping;

    public class TerrainTypeViewModel : IMapFrom<Race>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Distance { get; set; }

        public string OrganizerName { get; set; }

        public TerrainType TerrainType { get; set; }

        public bool IsParticipating { get; set; }
    }
}
