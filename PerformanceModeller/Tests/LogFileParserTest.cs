using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoFixture;
using Moq;
using NUnit.Framework;

namespace PerformanceModeller.Tests
{
    [TestFixture]
    public class LogFileParserTest
    {
        [Test]
        public void ParserThrowsExceptionWhenFileLocationIsIncorrect()
        {
            Assert.Throws<FileNotFoundException>(() => this.parser.SamplePerformance("./nonExistentFile.txt"));
        }

        [Test]
        public void ParserReturnsAllSamplesFromLogFile()
        {
            var lines = GetFileLines(path);
            var samples = CreateRandomSamples(lines.Count());
            
            for (int i = 0; i < lines.Count(); i++)
            {
                var line = lines.ElementAt(i);
                var sample = samples.ElementAt(i);
                this.readerMock.Setup(r => r.CreateSampleFromLine(line)).Returns(sample);
            }

            var res = this.parser.SamplePerformance(path);
            
            Assert.That(res, Is.EquivalentTo(samples));
        }


        [SetUp]
        public void Init()
        {
            this.path = TestContext.CurrentContext.TestDirectory + "/Tests/LogFile.txt";
            this.readerMock = new Mock<IPerformanceSampleReader>();
            this.parser = new LogFileParser(this.readerMock.Object);
        }

        private IEnumerable<string> GetFileLines(string fileLoc)
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

        private IEnumerable<PerformanceSample> CreateRandomSamples(int length)
        {
            var fixture = new Fixture();
            return Enumerable.Repeat(0, length).Select(_ => fixture.Create<PerformanceSample>()).ToList();
        }
        
        private LogFileParser parser;
        private Mock<IPerformanceSampleReader> readerMock;
        private string path;
    }
}