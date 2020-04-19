using System;
using TichTich.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TichTich.Data;

namespace ReadRSSFeed.Controllers
{
    public class RssController : Controller
    {
        private readonly ApplicationDbContext db;

        public RssController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ActionResult Index()
        {
            var RSSURL = this.db.Sources.Select(x => x.SourceUrl).ToList();
            WebClient wclient = new WebClient();
            List<News> allNews = new List<News>();

            foreach (var url in RSSURL)
            {
                string RSSData = wclient.DownloadString(url);

                XDocument xml = XDocument.Parse(RSSData);
                var RSSFeedData = from x in xml.Descendants("item")
                              select new News
                              {
                                  Title = (string)x.Element("title"),
                                  Source = (string)x.Element("link"),
                                  Description = (string)x.Element("description"),
                                  PubDate = (DateTime)x.Element("pubDate"),
                              };
                allNews.AddRange(RSSFeedData);
            }

            this.ViewBag.RSSFeed = allNews.OrderByDescending(x => x.PubDate);

            return this.View();
        }
    }
}
