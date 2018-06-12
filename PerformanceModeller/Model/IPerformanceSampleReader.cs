using System.Text.RegularExpressions;

namespace PerformanceModeller.Model
{
    public interface IPerformanceSampleReader
    {
        PerformanceSample CreateSampleFromLine(string line, Regex regex, int groupIndex);
    }
}