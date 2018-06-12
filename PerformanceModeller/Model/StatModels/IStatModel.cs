using System;
using System.Collections.Generic;

namespace PerformanceModeller.Model.StatModels
{
    public interface IStatModel
    {
        string GenerateCode(string modelName);

        PerformanceSample Sample();
    }
}