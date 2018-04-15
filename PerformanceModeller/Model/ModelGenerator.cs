using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceModeller.Model
{
    public class ModelGenerator : IModelGenerator
    {
        public string CreateModel(IEnumerable<PerformanceSample> samples, string modelName)
        {
            var model = new StringBuilder("using System;\r\n");
            model.AppendLine("using System.Collections.Generic;");
            model.AppendLine("using System.Linq;");
            model.AppendLine();
            
            model.AppendLine("namespace Performance.Models");
            model.AppendLine("{");
            model.AppendLine($"\tpublic class {modelName} : IPerformanceModel");
            model.AppendLine("\t{");
            model.AppendLine("\t\tprivate IEnumerable<double> samples;");
            model.AppendLine("\t\tprivate Random rand;");
            model.AppendLine();
            model.AppendLine($"\t\tpublic {modelName}()");
            model.AppendLine("\t\t{");
            model.AppendLine("\t\t\tthis.samples = new List<double>");
            model.AppendLine("\t\t\t{");

            var performanceSamples = samples.ToList();
            for (var i = 0; i < performanceSamples.Count; i++)
            {
                model.Append($"\t\t\t\t{performanceSamples.ElementAt(i).Duration.TotalMilliseconds}");
                if (i != performanceSamples.Count - 1) model.Append(",");
                model.AppendLine();
            }
            
            model.AppendLine("\t\t\t};");
            model.AppendLine("\t\t\tthis.rand = new Random();");
            model.AppendLine("\t\t}");
            model.AppendLine();
            model.AppendLine("\t\tpublic double DrawTime()");
            model.AppendLine("\t\t{");
            model.AppendLine("\t\t\treturn samples.ElementAt(rand.Next(samples.Count()));");
            model.AppendLine("\t\t}");
            model.AppendLine("\t}");
            model.AppendLine("}");

            return model.ToString();
        }
    }
}