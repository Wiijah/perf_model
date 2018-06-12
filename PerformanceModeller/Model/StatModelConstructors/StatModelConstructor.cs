using System;
using PerformanceModeller.Model.StatModels;

namespace PerformanceModeller.Model.StatModelConstructors
{
    public abstract class StatModelConstructor : IStatModelConstructor
    {
        public abstract string Label { get; }
        
        public abstract IStatModel FromSamples(ISamplesRepository samplesRepository);

        public override string ToString()
        {
            return Label;
        }
    }
}