using System;
using System.Collections.Generic;
using System.IO;

namespace PerformanceModeller
{
    public class LogFileParser : IFileParser
    {
        public LogFileParser(IPerformanceSampleReader reader)
        {
            this.reader = reader;
        }
        
        public IEnumerable<PerformanceSample> SamplePerformance(string logFileLocation)
        {
            if (!File.Exists(logFileLocation)) throw new FileNotFoundException("Tried to open non-existent Log file");

            IList<PerformanceSample> samples = new List<PerformanceSample>();
            
            string line;
            var logFile = new StreamReader(logFileLocation);

            while ((line = logFile.ReadLine()) != null)
            {
                samples.Add(reader.CreateSampleFromLine(line));
            }

            return samples;
        }

        private IPerformanceSampleReader reader;
    }
}