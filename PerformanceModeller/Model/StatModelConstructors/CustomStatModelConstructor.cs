using PerformanceModeller.Model.StatModels;

namespace PerformanceModeller.Model.StatModelConstructors
{
    public class CustomStatModelConstructor : StatModelConstructor
    {
        public override string Label => "Custom";
        
        public override IStatModel FromSamples(ISamplesRepository samplesRepository)
        {
            return new CustomStatModel(samplesRepository.GetSamples());
        }
    }
}