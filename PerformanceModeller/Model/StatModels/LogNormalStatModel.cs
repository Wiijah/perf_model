using System;
using System.Text;

namespace PerformanceModeller.Model.StatModels
{
    public class LogNormalStatModel : IStatModel
    {
        public LogNormalStatModel(double logScale, double shape)
        {
            this.logScale = logScale;
            this.shape = shape;
            
            this.normal = new NormalStatModel(0, 1);
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
            model.AppendLine("\t\tprivate IPerformanceModel logNormalModel;");
            model.AppendLine();
            model.AppendLine($"\t\tpublic {modelName}()");
            model.AppendLine("\t\t{");
            model.AppendLine($"\t\t\tthis.logNormalModel = new LogNormalDistributionPerformanceModel({logScale}, {shape});");
            model.AppendLine("\t\t}");
            model.AppendLine();
            model.AppendLine("\t\tpublic double DrawTime()");
            model.AppendLine("\t\t{");
            model.AppendLine("\t\t\treturn this.logNormalModel.DrawTime();");
            model.AppendLine("\t\t}");
            model.AppendLine("\t}");
            model.AppendLine("}");

            return model.ToString();
        }

        public PerformanceSample Sample()
        {
            var normalMillis = normal.Sample().Duration.TotalMilliseconds;
            var logNormalMillis = Math.Exp(logScale + shape * normalMillis);
            
            return new PerformanceSample {Duration = new TimeSpan(0, 0, 0, 0, (int) logNormalMillis)};
        }

        private readonly double logScale;
        private readonly double shape;
        private readonly NormalStatModel normal;
    }
}