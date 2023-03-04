using System;
using System.Linq;
using static TestingSpan.StringExtensions;

namespace TestingSpan
{
    /// <summary>
    /// Word Enumerator that can be used in a foreach statement
    /// Must be a ref struct as it contains ReadOnlySpan<char> field & property
    /// </summary>
    /// <remarks>
    /// Thanks to Meziantou, whose blog post inspired me:
    /// https://www.meziantou.net/split-a-string-into-lines-without-allocation.htm
    /// </remarks>
    public ref struct WordEnumerator
    {
        private ReadOnlySpan<char> _remainingSpan;

        public WordEnumerator(ReadOnlySpan<char> strSpan)
        {
            _remainingSpan = strSpan;
            Current = default;
        }

        public ReadOnlySpan<char> Current { get; private set; } // For compatibility with foreach operator
        public WordEnumerator GetEnumerator() => this;          // For compatibility with foreach operator

        public bool MoveNext()
        {
            var span = _remainingSpan;
            if (span.Length == 0)
                return false;

            // Find the start of a separator series
            var indexStart = span.IndexOfAny(WordSeparators);
            if (indexStart == -1) // _remainingSpan contains a single word
            {
                _remainingSpan = ReadOnlySpan<char>.Empty;
                Current = span;
                return true;
            }

            // Find the end of the separator series
            var indexEnd = indexStart;
            while (indexEnd < span.Length - 1 && WordSeparators.Contains(span[indexEnd + 1]))
            {
                indexEnd++;
            }

            _remainingSpan = span[(indexEnd + 1)..];

            // If this is an empty word, get the next one immediately
            if (indexStart == 0)
                return MoveNext();

            Current = span[..indexStart];
            return true;
        }
    }
}
