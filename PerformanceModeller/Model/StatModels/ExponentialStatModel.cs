using System;
using System.Text;

namespace PerformanceModeller.Model.StatModels
{
    public class ExponentialStatModel : IStatModel
    {
        public ExponentialStatModel(double rate)
        {
            this.rate = rate;
            this.random = new Random();
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
            model.AppendLine("\t\tprivate IPerformanceModel exponentialModel;");
            model.AppendLine();
            model.AppendLine($"\t\tpublic {modelName}()");
            model.AppendLine("\t\t{");
            model.AppendLine($"\t\t\tthis.exponentialModel = new ExponentialDistributionPerformanceModel({rate});");
            model.AppendLine("\t\t}");
            model.AppendLine();
            model.AppendLine("\t\tpublic double DrawTime()");
            model.AppendLine("\t\t{");
            model.AppendLine("\t\t\treturn this.exponentialModel.DrawTime();");
            model.AppendLine("\t\t}");
            model.AppendLine("\t}");
            model.AppendLine("}");

            return model.ToString();
        }

        public PerformanceSample Sample()
        {
            var r = random.NextDouble();
            while (r == 0.0)
            {
                r = random.NextDouble();
            }

            return new PerformanceSample {Duration = new TimeSpan(0, 0, 0, 0, (int) (-Math.Log(r) / rate))};
        }

        private readonly double rate;
        private readonly Random random;
    }
}