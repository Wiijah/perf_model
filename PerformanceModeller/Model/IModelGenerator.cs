using System.Collections.Generic;

namespace PerformanceModeller.Model
{
    public interface IModelGenerator
    {
        string CreateModel(IEnumerable<PerformanceSample> samples, string modelName);
    }
}