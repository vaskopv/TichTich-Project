using System;
using System.Collections.Generic;
using TichTich.Data.Common.Models;

namespace TichTich.Data.Models
{
    public class News : BaseDeletableModel<int>
    {
        public News()
        {
            this.Sources = new HashSet<Source>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Source { get; set; }

        public DateTime PubDate { get; set; }

        public ICollection<Source> Sources { get; set; }
    }
}
