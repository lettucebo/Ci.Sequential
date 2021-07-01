using System;

namespace Ci.Sequential
{
    /// <summary>
    /// Static methods to get an MSSQL-order variant COMB Guid for an easy way.
    /// </summary>
    public static class GuidSequential
    {
        private static readonly SeqGuid SeqGuid = new SeqGuid();

        /// <summary>
        /// Initializes a new instance of the sequential <see cref="T:System.Guid" /> structure.
        /// </summary>
        /// <returns></returns>
        public static System.Guid NewGuid() => SeqGuid.Next();

        /// <summary>
        /// A read-only instance of the <see cref="T:System.Guid" /> structure whose value is all zeros.
        /// </summary>
        public static System.Guid Empty => System.Guid.Empty;
    }
}
