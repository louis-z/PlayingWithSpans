using System;
using System.Runtime.InteropServices;

namespace IntroToSpans
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
            var byteArray = new byte[100];
            Span<byte> byteSpanOnManagedMemory = byteArray;

            // native memory
            var nativeMemory = Marshal.AllocHGlobal(100);
            Span<byte> byteSpanOnNativeMemory;
            unsafe
            {
                byteSpanOnNativeMemory = new Span<byte>(nativeMemory.ToPointer(), 100);
            }

            SafeSum(byteSpanOnNativeMemory);
            Marshal.FreeHGlobal(nativeMemory);

            // stack memory
            Span<byte> byteSpanOnStackMemory = stackalloc byte[100];
            SafeSum(byteSpanOnStackMemory);
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
