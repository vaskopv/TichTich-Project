using System;
using System.Collections.Generic;
using System.Text;

namespace TichTich.Web.ViewModels
{
    public class RssFeedViewModel
    {
        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string Source { get; set; }

        public DateTime PubDate { get; set; }
    }
}
