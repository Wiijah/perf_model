using System.Linq;
using PerformanceModeller.Model.StatModels;

namespace PerformanceModeller.Model.StatModelConstructors
{
    public class UniformStatModelConstructor : StatModelConstructor
    {
        public override string Label => "Uniform";
        
        public override IStatModel FromSamples(ISamplesRepository samplesRepository)
        {
            var performanceSamples = samplesRepository.GetSamples() as PerformanceSample[] ?? samplesRepository.GetSamples().ToArray();

            var min = performanceSamples.Min(t => t.Duration.TotalMilliseconds);
            var max = performanceSamples.Max(t => t.Duration.TotalMilliseconds);
            
            return new UniformStatModel(min, max);
        }
    }
}