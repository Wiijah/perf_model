using System;
using AutoFixture;
using NUnit.Framework;

namespace PerformanceModeller.Tests
{
    [TestFixture]
    public class TestDemoPerformanceSampleReader
    {
        [Test]
        public void ReaderThrowsExceptionIfFormatWrong()
        {
            var input = "Wrong format";
            
            Assert.Throws<FormatException>(() => this.reader.CreateSampleFromLine(input));
        }

        [Test]
        public void ReaderReturnsCorrectSampleFromRightFormat()
        {
            var duration = new Fixture().Create<TimeSpan>();
            var input = "Time taken: " + duration;

            var res = this.reader.CreateSampleFromLine(input);
            
            Assert.AreEqual(res.Duration, duration);
        }

        [SetUp]
        public void Init()
        {
            this.reader = new DemoPerformanceSampleReader();
        }

        private DemoPerformanceSampleReader reader;
    }
}