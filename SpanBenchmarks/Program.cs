#if DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
#else
using BenchmarkDotNet.Running;
#endif

namespace TestingSpan
{
    internal static class Program
    {
        private static void Main()
        {
#if DEBUG
            var wordsFromSplit = new List<string>();
            var wordsFromSpan = new List<string>();

            SpanBenchmarks.UseSplit(wordsFromSplit);
            SpanBenchmarks.UseSpan(wordsFromSpan);

            if (wordsFromSplit.SequenceEqual(wordsFromSpan))
            {
                Console.WriteLine("The text was broken down into {0} words using both techniques; sequences are identical", wordsFromSpan.Count);
            }
            else
            {
                Console.Error.WriteLine("The text was broken down into {0} words using Split", wordsFromSplit.Count);
                Console.Error.WriteLine("The text was broken down into {0} words using Span", wordsFromSpan.Count);
            }
#else
            BenchmarkRunner.Run<SpanBenchmarks>();
#endif
        }
    }
}
