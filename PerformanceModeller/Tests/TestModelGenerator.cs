using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PerformanceModeller.Model;

namespace PerformanceModeller.Tests
{
    [TestFixture]
    public class TestModelGenerator
    {
        [Test]
        public void CreateModelCreatesCorrectModel()
        {
            var modelGenerator = new ModelGenerator();
            var expected = 
                Utils.GetFileLines(TestContext.CurrentContext.TestDirectory + "/Tests/DemoModel.txt")
                     .Aggregate(String.Empty, (s, s1) =>
                    {
                        s = s + s1 + "\r\n";
                        return s;
                    });
            var samples = new List<PerformanceSample>
            {
                new PerformanceSample {Duration = new TimeSpan(0, 0, 0, 0, 1)},
                new PerformanceSample {Duration = new TimeSpan(0, 0, 0, 0, 2)},
                new PerformanceSample {Duration = new TimeSpan(0, 0, 0, 0, 3)},
                new PerformanceSample {Duration = new TimeSpan(0, 0, 0, 0, 4)},
                new PerformanceSample {Duration = new TimeSpan(0, 0, 0, 0, 5)}
            };
            var modelName = "DemoModel";

            var actual = modelGenerator.CreateModel(samples, modelName);
            
            Assert.AreEqual(expected, actual);
        }
    }
}