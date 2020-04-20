using System;
using System.Collections.Generic;
using System.Text;
using TichTich.Web.Controllers;

namespace TichTich.Services.Data
{
    public class CalculatorsService : ICalculatorsService
    {
        public double Calculate(PredictorInputViewModel input)
        {
            var time = TimeSpan.Parse(input.Hours.ToString() + ":" + input.Minutes.ToString() + ":" + input.Seconds.ToString());
            var result = time.TotalMinutes * Math.Pow(2, 1.15);

            return result;
        }
    }
}
