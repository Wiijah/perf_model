using System;
using System.Linq;
using PerformanceModeller.Model.StatModels;

namespace PerformanceModeller.Model.StatModelConstructors
{
    public class NormalStatModelConstructor : StatModelConstructor
    {
        public override string Label => "Normal";
        
        public override IStatModel FromSamples(ISamplesRepository samplesRepository)
        {
            var performanceSamples = samplesRepository.GetSamples() as PerformanceSample[] ?? samplesRepository.GetSamples().ToArray();

            var estMean = performanceSamples.Sum(t => t.Duration.TotalMilliseconds);
            estMean = estMean / performanceSamples.Length;
            
            var estVariance = performanceSamples.Sum(t => Math.Pow((t.Duration.TotalMilliseconds - estMean), 2));
            estVariance = estVariance / performanceSamples.Length;
  
            return new NormalStatModel(estMean, Math.Sqrt(estVariance));
        }
    }
}