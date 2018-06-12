using PerformanceModeller.Model.StatModels;

namespace PerformanceModeller.Model.StatModelConstructors
{
    public interface IStatModelConstructor
    {
        IStatModel FromSamples(ISamplesRepository samplesRepository);
    }
}