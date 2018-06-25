using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceModeller.Model.StatModels
{
    public class NormalStatModel : IStatModel
    {
        public NormalStatModel(double mean, double stdDev)
        {
            this.mean = mean;
            this.stdDev = stdDev;
        }
        
        public string GenerateCode(string modelName)
        {
            var model = new StringBuilder("using System;\r\n");
            model.AppendLine("using System.Collections.Generic;");
            model.AppendLine("using System.Linq;");
            model.AppendLine();
            
            model.AppendLine("namespace Performance.Models");
            model.AppendLine("{");
            model.AppendLine($"\tpublic class {modelName} : IPerformanceModel");
            model.AppendLine("\t{");
            model.AppendLine("\t\tprivate IPerformanceModel gaussianModel;");
            model.AppendLine();
            model.AppendLine($"\t\tpublic {modelName}()");
            model.AppendLine("\t\t{");
            model.AppendLine($"\t\t\tthis.gaussianModel = new GaussianDistributionPerformanceModel({mean}, {stdDev});");
            model.AppendLine("\t\t}");
            model.AppendLine();
            model.AppendLine("\t\tpublic double DrawTime()");
            model.AppendLine("\t\t{");
            model.AppendLine("\t\t\treturn this.gaussianModel.DrawTime();");
            model.AppendLine("\t\t}");
            model.AppendLine("\t}");
            model.AppendLine("}");

            return model.ToString();
        }

        public PerformanceSample Sample()
        {
            double x1 = 1 - random.NextDouble();
            double x2 = 1 - random.NextDouble();

            double y1 = Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2);
            double dev = y1 * stdDev;
            return new PerformanceSample {Duration = new TimeSpan(0, 0, 0, 0, (int) (dev + mean))};
        }

        private readonly Random random = new Random();
        private readonly double mean;
        private readonly double stdDev;
    }
}