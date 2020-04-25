namespace TichTich.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TichTich.Web.ViewModels.Results;

    public interface IOrganizersService
    {
        ICollection<ResultsViewModel> Results(int id);

        Task EnterTimeAsync(FinishTimeViewModel finishTime);
    }
}
