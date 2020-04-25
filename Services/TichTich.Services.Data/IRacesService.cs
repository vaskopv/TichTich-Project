namespace TichTich.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TichTich.Data.Models;
    using TichTich.Data.Models.Enums;
    using TichTich.Web.ViewModels.Races;

    public interface IRacesService
    {
        public IEnumerable<Race> GetByTerrainType(string type);

        T GetByType<T>(string type);

        public ICollection<ByIdViewModel> GetByOrganizerId(string id);

        public ByIdViewModel GetByRaceId(int id, string userId);

        Task<int> CreateAsync(string name, double distance, string description, string orgnizerId, TerrainType terrainType);

        Task<int> EditAsync(EditRaceInputViewModel race);

        public EditRaceInputViewModel Edit(int id);

        public Task Delete(int id);
    }
}
