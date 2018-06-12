using System;
using System.Text.RegularExpressions;
using AutoFixture;
using NUnit.Framework;
using PerformanceModeller.Model;

namespace PerformanceModeller.Tests
{
    [TestFixture]
    public class TestRegexPerformanceSampleReader
    {
        [Test]
        public void ReaderThrowsExceptionIfFormatWrong()
        {
            var input = "Wrong format";
            
            Assert.Throws<FormatException>(() => this.reader.CreateSampleFromLine(input, this.regex, this.groupIndex));
        }

        [Test]
        public void ReaderReturnsCorrectSampleFromRightFormat()
        {
            var duration = new Fixture().Create<TimeSpan>();
            var input = "Time taken: " + duration;

            var res = this.reader.CreateSampleFromLine(input, this.regex, this.groupIndex);
            
            Assert.AreEqual(res.Duration, duration);
        }

        [SetUp]
        public void Init()
        {
            this.reader = new RegexPerformanceSampleReader();
            this.regex = new Regex("Time taken: (.+)");
            this.groupIndex = 1;
        }

        private RegexPerformanceSampleReader reader;
        private Regex regex;
        private int groupIndex;
    }
}