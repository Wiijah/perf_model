using System;
using System.Text;

namespace PerformanceModeller.Model.StatModels
{
    public class UniformStatModel : IStatModel
    {
        public UniformStatModel(double min, double max)
        {
            this.min = min;
            this.max = max;
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
            model.AppendLine("\t\tprivate IPerformanceModel uniformModel;");
            model.AppendLine();
            model.AppendLine($"\t\tpublic {modelName}()");
            model.AppendLine("\t\t{");
            model.AppendLine($"\t\t\tthis.uniformModel= new UniformDistributionPerformanceModel({min}, {max});");
            model.AppendLine("\t\t}");
            model.AppendLine();
            model.AppendLine("\t\tpublic double DrawTime()");
            model.AppendLine("\t\t{");
            model.AppendLine("\t\t\treturn this.uniformModel.DrawTime();");
            model.AppendLine("\t\t}");
            model.AppendLine("\t}");
            model.AppendLine("}");

            return model.ToString();
        }

        public PerformanceSample Sample()
        {
            int overflow = 0;
            double newMin = min;
            double newMax = max;

            while (newMin >= int.MaxValue)
            {
                overflow++;
                newMin -= int.MaxValue;
                newMax -= int.MaxValue;
            }
			
            var millis = random.Next((int) newMin, ((int) newMax) + 1) + int.MaxValue * overflow;
            return new PerformanceSample { Duration = new TimeSpan(0, 0, 0, 0, millis)};
        }

        private double min;
        private double max;
        private Random random;
    }
}