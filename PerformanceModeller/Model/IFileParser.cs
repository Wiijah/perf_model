using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PerformanceModeller.Model
{
    public interface IFileParser
    {
        IEnumerable<PerformanceSample> SamplePerformance(string logFileLocation, Regex regex, int groupIndex);
    }
}