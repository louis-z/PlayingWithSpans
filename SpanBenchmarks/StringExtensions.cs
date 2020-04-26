using System;

namespace TestingSpan
{
    public static class StringExtensions
    {
        public static readonly char[] WordSeparators = new[] { ' ', '\r', '\n', '\t' };

        public static WordEnumerator SplitIntoWords(this string str)
        {
            return new WordEnumerator(str.AsSpan());    // WordEnumerator is a struct -> no heap allocation here
        }
    }
}
