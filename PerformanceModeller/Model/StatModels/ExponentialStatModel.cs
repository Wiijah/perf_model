using System;

namespace PerformanceModeller.Model.StatModels
{
    public class ExponentialStatModel : IStatModel
    {
        public ExponentialStatModel(double rate)
        {
            this.rate = rate;
            this.random = new Random();
        }
        
        public string GenerateCode(string modelName)
        {
            throw new System.NotImplementedException();
        }

        public PerformanceSample Sample()
        {
            var r = random.NextDouble();
            while (r == 0.0)
            {
                r = random.NextDouble();
            }

            return new PerformanceSample {Duration = new TimeSpan(0, 0, 0, 0, (int) (-Math.Log(r) / rate))};
        }

        private readonly double rate;
        private readonly Random random;
    }
}