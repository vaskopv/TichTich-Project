using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TichTich.Web.ViewModels;

namespace TichTich.Services.Data
{
    public interface IRssService
    {
        public IOrderedEnumerable<RssFeedViewModel> GetRssFeed(string searchString);
    }
}
