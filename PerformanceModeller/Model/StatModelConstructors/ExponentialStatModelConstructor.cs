using System.Linq;
using PerformanceModeller.Model.StatModels;

namespace PerformanceModeller.Model.StatModelConstructors
{
    public class ExponentialStatModelConstructor : StatModelConstructor
    {
        public override string Label => "Exponential";
        
        public override IStatModel FromSamples(ISamplesRepository samplesRepository)
        {
            var performanceSamples = samplesRepository.GetSamples() as PerformanceSample[] ?? samplesRepository.GetSamples().ToArray();

            var mle = performanceSamples.Length / performanceSamples.Sum(t => t.Duration.TotalMilliseconds); // TODO: Check
            
            return new ExponentialStatModel(mle);
        }
    } 
}