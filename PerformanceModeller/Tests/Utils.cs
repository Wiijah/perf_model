using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoFixture;
using PerformanceModeller.Model;

namespace PerformanceModeller.Tests
{
    public static class Utils
    {
        public static IEnumerable<string> GetFileLines(string fileLoc)
        {
            StreamReader file = new StreamReader(fileLoc);
            string line;

            IList<string> lines = new List<string>();
            
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }

            return lines;
        }

        public static IEnumerable<PerformanceSample> CreateRandomSamples(int length)
        {
            var fixture = new Fixture();
            return Enumerable.Repeat(0, length).Select(_ => fixture.Create<PerformanceSample>()).ToList();
        }
    }
}