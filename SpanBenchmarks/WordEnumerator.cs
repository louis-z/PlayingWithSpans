using System;
using System.Linq;
using static TestingSpan.StringExtensions;

namespace TestingSpan
{
    // Must be a ref struct as it contains ReadOnlySpan<char> field & property
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

            var index = span.IndexOfAny(WordSeparators);
            if (index == -1) // _remainingSpan is made up of a single word
            {
                _remainingSpan = ReadOnlySpan<char>.Empty;
                Current = span;
                return true;
            }

            // Find all consecutive word separators
            var index2 = index;
            while (index2 < span.Length - 1 && WordSeparators.Contains(span[index2 + 1]))
            {
                index2++;
            }

            var separatorSeriesLength = index2 - index + 1;
            _remainingSpan = span.Slice(index + separatorSeriesLength);

            // If this is an empty word, get the next one immediately
            if (index == 0)
                return MoveNext();

            Current = span.Slice(0, index);
            return true;
        }
    }
}
