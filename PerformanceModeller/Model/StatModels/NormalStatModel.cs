using System;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceModeller.Model.StatModels
{
    public class NormalStatModel : IStatModel
    {
        public NormalStatModel(double mean, double stdDev)
        {
            this.mean = mean;
            this.stdDev = stdDev;
        }
        
        public string GenerateCode(string modelName)
        {
            throw new System.NotImplementedException();
        }

        public PerformanceSample Sample()
        {
            double x1 = 1 - random.NextDouble();
            double x2 = 1 - random.NextDouble();

            double y1 = Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2);
            double dev = y1 * stdDev;
            return new PerformanceSample {Duration = new TimeSpan(0, 0, 0, 0, (int) (dev + mean))};
        }

        private readonly Random random = new Random();
        private readonly double mean;
        private readonly double stdDev;
    }
}