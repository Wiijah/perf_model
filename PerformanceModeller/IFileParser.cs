using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PerformanceModeller
{
    public interface IFileParser
    {
        IEnumerable<PerformanceSample> SamplePerformance(string logFileLocation);
    }
}