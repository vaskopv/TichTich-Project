using System;
using System.Collections.Generic;
using System.Text;
using TichTich.Web.Controllers;
using TichTich.Web.Controllers.Calculators;

namespace TichTich.Services.Data
{
    public class CalculatorsService : ICalculatorsService
    {
        public double Calculate(RaceTimeInputViewModel input)
        {
            var time = TimeSpan.Parse(input.Hours.ToString() + ":" + input.Minutes.ToString() + ":" + input.Seconds.ToString());
            var result = time.TotalMinutes * Math.Pow(2, 1.20);

            return result;
        }
    }
}
