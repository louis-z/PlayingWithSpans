#if DEBUG
using System;
using System.Linq;
#else
using BenchmarkDotNet.Running;
#endif

namespace TestingSpan;

internal static class Program
{
    private static void Main()
    {
#if DEBUG
        var bm = new SpanBenchmarks();
        var wordsFromSplit = bm.UseSplit();
        var wordsFromSpan = bm.UseSpan();

        Console.WriteLine($"Sequences are equal: {wordsFromSpan.SequenceEqual(wordsFromSplit)}");
#else
        BenchmarkRunner.Run<SpanBenchmarks>();
#endif
    }
}
