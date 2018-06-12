using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PerformanceModeller.Model;
using PerformanceModeller.Model.StatModels;

namespace PerformanceModeller.Tests
{
    [TestFixture]
    public class TestCustomStatModel
    {
        [Test]
        public void CreateModelCreatesCorrectModel()
        {
            var samples = new List<PerformanceSample>
            {
                new PerformanceSample {Duration = new TimeSpan(0, 0, 0, 0, 1)},
                new PerformanceSample {Duration = new TimeSpan(0, 0, 0, 0, 2)},
                new PerformanceSample {Duration = new TimeSpan(0, 0, 0, 0, 3)},
                new PerformanceSample {Duration = new TimeSpan(0, 0, 0, 0, 4)},
                new PerformanceSample {Duration = new TimeSpan(0, 0, 0, 0, 5)}
            };
            var customStatModel = new CustomStatModel(samples);
            
            var expected = 
                Utils.GetFileLines(TestContext.CurrentContext.TestDirectory + "/Tests/DemoModel.txt")
                     .Aggregate(String.Empty, (s, s1) =>
                    {
                        s = s + s1 + "\r\n";
                        return s;
                    });
            var modelName = "DemoModel";

            var actual = customStatModel.GenerateCode(modelName);
            
            Assert.AreEqual(expected, actual);
        }
    }
}