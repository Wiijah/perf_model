using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PerformanceModeller.Model
{
    public class SamplesRepository : ISamplesRepository
    {
        public SamplesRepository(IFileParser parser)
        {
            this.parser = parser;
            this.cachedSamples = new CachedItem<IEnumerable<PerformanceSample>>(() => this.parser.SamplePerformance(fileLoc, new Regex(regex), groupId));
        }
        
        public IEnumerable<PerformanceSample> GetSamples()
        {
            if (fileLoc != null && regex != null) return cachedSamples.Value;
            return null;
        }

        public void SetSamplingSource(string fileLoc, string regex, int groupId)
        {
            this.fileLoc = fileLoc;
            this.regex = regex;
            this.groupId = groupId;
            
            this.cachedSamples.Invalidate();
        }

        private readonly IFileParser parser;
        private string fileLoc;
        private string regex;
        private int groupId;

        private CachedItem<IEnumerable<PerformanceSample>> cachedSamples;
    }
}