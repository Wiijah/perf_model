using System;
using System.Linq;
using PerformanceModeller.Model.StatModels;

namespace PerformanceModeller.Model.StatModelConstructors
{
    public class LogNormalStatModelConstructor : StatModelConstructor
    {
        public override string Label => "LogNormal";
        
        public override IStatModel FromSamples(ISamplesRepository samplesRepository)
        {
            var performanceSamples = samplesRepository.GetSamples() as PerformanceSample[] ?? samplesRepository.GetSamples().ToArray();
            var logSamples =
                (from ps in performanceSamples
                select Math.Log(ps.Duration.TotalMilliseconds)).ToArray();

            var estLogScale = logSamples.Sum(t => t);
            estLogScale = estLogScale / logSamples.Length;
            
            var estShape = logSamples.Sum(t => Math.Pow(t - estLogScale, 2));
            estShape = estShape / logSamples.Length;
  
            return new LogNormalStatModel(estLogScale, Math.Sqrt(estShape));
        }
    }
}