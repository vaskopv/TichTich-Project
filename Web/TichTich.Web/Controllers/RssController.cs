namespace ReadRSSFeed.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using TichTich.Services.Data;

    public class RssController : Controller
    {
        private readonly IRssService rssService;

        public RssController(IRssService rssService)
        {
            this.rssService = rssService;
        }

        public ActionResult Index(string searchString)
        {
            var result = this.rssService.GetRssFeed(searchString);

            return this.View(result);
        }
    }
}
