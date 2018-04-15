using System;
using System.Text.RegularExpressions;

namespace PerformanceModeller.Model
{
    public class DemoPerformanceSampleReader : IPerformanceSampleReader
    {
        public PerformanceSample CreateSampleFromLine(string line)
        {
            var regex = new Regex("Time taken: (.+)");
            var match = regex.Match(line);

            if (match.Success)
            {
                var time = TimeSpan.Parse(match.Groups[1].Value);
                return new PerformanceSample {Duration = time};
            }
            
            throw new FormatException();
        }
    }
}