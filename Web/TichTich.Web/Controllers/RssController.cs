namespace ReadRSSFeed.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Xml.Linq;

    using Microsoft.AspNetCore.Mvc;
    using TichTich.Data;
    using TichTich.Services.Data;
    using TichTich.Web.ViewModels;
    using X.PagedList;

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
