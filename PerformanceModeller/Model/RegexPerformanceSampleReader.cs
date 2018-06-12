using System;
using System.Text.RegularExpressions;

namespace PerformanceModeller.Model
{
    public class RegexPerformanceSampleReader : IPerformanceSampleReader
    {
        public PerformanceSample CreateSampleFromLine(string line, Regex regex, int groupIndex)
        {
            var match = regex.Match(line);

            if (match.Success)
            {
                var time = TimeSpan.Parse(match.Groups[groupIndex].Value);
                return new PerformanceSample {Duration = time};
            }
            
            throw new FormatException();
        }
    }
}