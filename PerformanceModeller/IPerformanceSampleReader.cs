namespace PerformanceModeller
{
    public interface IPerformanceSampleReader
    {
        PerformanceSample CreateSampleFromLine(string line);
    }
}