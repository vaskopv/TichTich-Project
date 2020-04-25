namespace TichTich.Services.Data
{
    using System;

    using TichTich.Web.Controllers.Calculators;

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
