using System;
using System.Collections.Generic;
using System.Text;
using TichTich.Web.Controllers;

namespace TichTich.Services.Data
{
    public interface ICalculatorsService
    {
        public double Calculate(PredictorInputViewModel input);
    }
}
