using TichTich.Data.Common.Models;

namespace TichTich.Data.Models
{
    public class Source : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string SourceUrl { get; set; }
    }
}