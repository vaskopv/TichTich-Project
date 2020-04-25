namespace TichTich.Services.Data
{
    using System.Linq;

    using TichTich.Web.ViewModels;

    public interface IRssService
    {
        public IOrderedEnumerable<RssFeedViewModel> GetRssFeed(string searchString);
    }
}
