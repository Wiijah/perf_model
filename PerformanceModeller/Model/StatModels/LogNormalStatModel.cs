using System;

namespace PerformanceModeller.Model.StatModels
{
    public class LogNormalStatModel : IStatModel
    {
        public LogNormalStatModel(double logScale, double shape)
        {
            this.logScale = logScale;
            this.shape = shape;
            
            this.normal = new NormalStatModel(0, 1);
        }
        
        public string GenerateCode(string modelName)
        {
            throw new System.NotImplementedException();
        }

        public PerformanceSample Sample()
        {
            var normalMillis = normal.Sample().Duration.TotalMilliseconds;
            var logNormalMillis = Math.Exp(logScale + shape * normalMillis);
            
            return new PerformanceSample {Duration = new TimeSpan(0, 0, 0, 0, (int) logNormalMillis)};
        }

        private readonly double logScale;
        private readonly double shape;
        private readonly NormalStatModel normal;
    }
}