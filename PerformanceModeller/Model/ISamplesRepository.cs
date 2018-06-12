using System.Collections.Generic;

namespace PerformanceModeller.Model
{
    public interface ISamplesRepository
    {
        IEnumerable<PerformanceSample> GetSamples();
        void SetSamplingSource(string fileLoc, string regex, int groupId);
    }
}