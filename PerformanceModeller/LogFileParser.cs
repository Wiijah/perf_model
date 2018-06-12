using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using PerformanceModeller.Model;

namespace PerformanceModeller
{
    public class LogFileParser : IFileParser
    {
        public LogFileParser(IPerformanceSampleReader reader)
        {
            this.reader = reader;
        }
        
        public IEnumerable<PerformanceSample> SamplePerformance(string logFileLocation, Regex regex, int groupIndex)
        {
            if (!File.Exists(logFileLocation)) throw new FileNotFoundException("Tried to open non-existent Log file");

            IList<PerformanceSample> samples = new List<PerformanceSample>();
            
            string line;
            var logFile = new StreamReader(logFileLocation);

            while ((line = logFile.ReadLine()) != null)
            {
                samples.Add(reader.CreateSampleFromLine(line, regex, groupIndex));
            }

            return samples;
        }

        private readonly IPerformanceSampleReader reader;
    }
}