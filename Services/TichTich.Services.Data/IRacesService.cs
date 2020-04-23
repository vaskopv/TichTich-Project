namespace TichTich.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using TichTich.Data.Models;
    using TichTich.Data.Models.Enums;
    using TichTich.Web.ViewModels.Races;

    public interface IRacesService
    {
        public IEnumerable<Race> GetByTerrainType(string type);

        T GetByType<T>(string type);

        public ICollection<ByIdViewModel> GetByOrganizerId(string id);

        public ByIdViewModel GetByRaceId(int id);

        Task<int> CreateAsync(string name, double distance, string description, string orgnizerId, TerrainType terrainType);

        Task<int> EditAsync(CreateRaceInputModel race);
    }
}