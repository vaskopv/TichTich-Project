using System;
using System.Collections.Generic;
using System.Text;
using TichTich.Data.Models;
using X.PagedList;

namespace TichTich.Web.ViewModels.Races
{
    public class CombinedViewModel
    {
        public RacesSortViewModel SortedModel { get; set; }

        public IPagedList<ByIdViewModel> Races { get; set; }
    }
}
