using System.Collections.Generic;

namespace PerformanceModeller.Model
{
    public interface IFileParser
    {
        IEnumerable<PerformanceSample> SamplePerformance(string logFileLocation);
    }
}