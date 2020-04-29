using BenchmarkDotNet.Running;

namespace TestingSpan
{
    internal static class Program
    {
        private static void Main()
        {
            BenchmarkRunner.Run<SpanBenchmarks>();
        }
    }
}
