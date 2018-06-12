using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AutoFixture;
using Moq;
using NUnit.Framework;
using PerformanceModeller.Model;

namespace PerformanceModeller.Tests
{
    [TestFixture]
    public class LogFileParserTest
    {
        [Test]
        public void ParserThrowsExceptionWhenFileLocationIsIncorrect()
        {
            Assert.Throws<FileNotFoundException>(() => this.parser.SamplePerformance("./nonExistentFile.txt", REGEX, GROUP_ID));
        }

        [Test]
        public void ParserReturnsAllSamplesFromLogFile()
        {
            var lines = Utils.GetFileLines(path);
            var samples = Utils.CreateRandomSamples(lines.Count());
            
            for (int i = 0; i < lines.Count(); i++)
            {
                var line = lines.ElementAt(i);
                var sample = samples.ElementAt(i);
                this.readerMock.Setup(r => r.CreateSampleFromLine(line, REGEX, GROUP_ID)).Returns(sample);
            }

            var res = this.parser.SamplePerformance(path, REGEX, GROUP_ID);
            
            Assert.That(res, Is.EquivalentTo(samples));
        }


        [SetUp]
        public void Init()
        {
            this.path = TestContext.CurrentContext.TestDirectory + "/Tests/LogFile.txt";
            this.readerMock = new Mock<IPerformanceSampleReader>();
            this.parser = new LogFileParser(this.readerMock.Object);
        }
        
        private LogFileParser parser;
        private Mock<IPerformanceSampleReader> readerMock;
        private string path;

        private readonly Regex REGEX = new Regex("Time taken: (.+)");
        private const int GROUP_ID = 1;
    }
}