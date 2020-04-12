namespace TichTich.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using TichTich.Data.Models;
    using TichTich.Data.Models.Enums;

    public interface IRacesService
    {
        public IEnumerable<Race> GetByTerrainType(string type);

        T GetByType<T>(string type);

        public IEnumerable<Race> GetAllRaces();

        Task<int> CreateAsync(string name, string description, string orgnizerId, TerrainType terrainType);
    }
}