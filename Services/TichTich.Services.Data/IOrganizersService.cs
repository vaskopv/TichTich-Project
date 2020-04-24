using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TichTich.Web.ViewModels.Results;

namespace TichTich.Services.Data
{
    public interface IOrganizersService
    {
        ICollection<ResultsViewModel> Results(int id);

        Task EnterTimeAsync(FinishTimeViewModel finishTime);
    }
}
