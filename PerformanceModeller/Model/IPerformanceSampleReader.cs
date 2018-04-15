namespace PerformanceModeller.Model
{
    public interface IPerformanceSampleReader
    {
        PerformanceSample CreateSampleFromLine(string line);
    }
}