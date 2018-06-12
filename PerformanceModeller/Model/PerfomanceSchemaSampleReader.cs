using System;
using System.Text.RegularExpressions;

namespace PerformanceModeller.Model
{
    public class PerfomanceSchemaSampleReader : IPerformanceSampleReader
    {
        public PerformanceSample CreateSampleFromLine(string line, Regex regex, int groupIndex)
        {
            var match = regex.Match(line);

            if (match.Success)
            {
                var duration = match.Groups[groupIndex].Value;
                var groups = duration.Split('.');
                
                var time = new TimeSpan(0, 0, 0, Convert.ToInt32(groups[0]), Convert.ToInt32(groups[1].Substring(0, 3)));
                return new PerformanceSample {Duration = time};
            }
            
            throw new FormatException();
        }
    }
}