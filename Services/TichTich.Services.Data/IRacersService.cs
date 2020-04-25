namespace TichTich.Services.Data
{
    using System.Threading.Tasks;

    public interface IRacersService
    {
        public Task Participate(int raceId, string userId);
    }
}
