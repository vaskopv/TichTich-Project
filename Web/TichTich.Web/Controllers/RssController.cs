namespace ReadRSSFeed.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Xml.Linq;

    using Microsoft.AspNetCore.Mvc;
    using TichTich.Data;
    using TichTich.Data.Models;
    using TichTich.Web.ViewModels;
    using X.PagedList;

    public class RssController : Controller
    {
        private readonly ApplicationDbContext db;

        public RssController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ActionResult Index(string searchString)
        {
            var rssUrl = this.db.Sources.Select(x => x.SourceUrl).ToList();

            WebClient wclient = new WebClient();
            List<RssFeedViewModel> allNews = new List<RssFeedViewModel>();

            foreach (var url in rssUrl)
            {
                string rssData = wclient.DownloadString(url);

                XDocument xml = XDocument.Parse(rssData);
                var rssFeedData = from x in xml.Descendants("item")
                                  select new RssFeedViewModel
                                  {
                                      Title = (string)x.Element("title"),
                                      Source = (string)x.Element("link"),
                                      ImageUrl = this.db.Sources
                                      .Where(x => x.SourceUrl == url)
                                      .Select(x => x.ImageUrl)
                                      .FirstOrDefault(),
                                      PubDate = (DateTime)x.Element("pubDate"),
                                  };

                allNews.AddRange(rssFeedData);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                allNews = allNews.Where(s => s.Title.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            var sortedNews = allNews.OrderByDescending(x => x.PubDate);

            return this.View(sortedNews);

        }
    }
}
