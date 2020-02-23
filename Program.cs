using System;
using System.Runtime.InteropServices;

namespace PlayingWithSpans
{
    /// <summary>
    /// Getting familiar with Span<T>...
    /// </summary>
    /// <see href="https://github.com/dotnet/corefxlab/blob/master/docs/specs/span.md"/>
    /// <see href="https://docs.microsoft.com/en-us/archive/msdn-magazine/2018/january/csharp-all-about-span-exploring-a-new-net-mainstay"/>
    internal class Program
    {
        private static void Main()
        {
            TestSpanWithAllMemoryTypes();
        }

        private static void TestSpanWithAllMemoryTypes()
        {
            // managed memory
            var arrayMemory = new byte[100];
            var arraySpan = new Span<byte>(arrayMemory);

            // native memory
            var nativeMemory = Marshal.AllocHGlobal(100);
            Span<byte> nativeSpan;
            unsafe
            {
                nativeSpan = new Span<byte>(nativeMemory.ToPointer(), 100);
            }

            SafeSum(nativeSpan);
            Marshal.FreeHGlobal(nativeMemory);

            // stack memory
            Span<byte> stackSpan = stackalloc byte[100];
            SafeSum(stackSpan);
        }

        // this method does not care what kind of memory it works on
        private static ulong SafeSum(Span<byte> bytes)
        {
            ulong sum = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                sum += bytes[i];
            }

            return sum;
        }
    }
}
