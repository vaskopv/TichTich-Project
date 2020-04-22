using System;
using System.Collections.Generic;
using System.Text;
using TichTich.Web.Controllers;
using TichTich.Web.Controllers.Calculators;

namespace TichTich.Services.Data
{
    public interface ICalculatorsService
    {
        public double Calculate(RaceTimeInputViewModel input);
    }
}
