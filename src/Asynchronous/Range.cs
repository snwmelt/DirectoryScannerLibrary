

namespace DirectoryScanner.Asynchronous
{
    /// <summary>
    /// Represents a range of integer values.
    /// </summary>
    internal struct Range
    {
        /// <summary>
        /// The first integer value in this range.
        /// </summary>
        public readonly int First;

        /// <summary>
        /// The last integer value in this range.
        /// </summary>
        public readonly int Last;

        public Range(int First, int Last)
        {
            this.First = First;
            this.Last  = Last;
        }
    }
}
