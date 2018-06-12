using System;

namespace PerformanceModeller.Model.StatModels
{
    public class UniformStatModel : IStatModel
    {
        public UniformStatModel(double min, double max)
        {
            this.min = min;
            this.max = max;
            this.random = new Random();
        }
        
        public string GenerateCode(string modelName)
        {
            throw new System.NotImplementedException();
        }

        public PerformanceSample Sample()
        {
            int overflow = 0;
            double newMin = min;
            double newMax = max;

            while (newMin >= int.MaxValue)
            {
                overflow++;
                newMin -= int.MaxValue;
                newMax -= int.MaxValue;
            }
			
            var millis = random.Next((int) newMin, ((int) newMax) + 1) + int.MaxValue * overflow;
            return new PerformanceSample { Duration = new TimeSpan(0, 0, 0, 0, millis)};
        }

        private double min;
        private double max;
        private Random random;
    }
}