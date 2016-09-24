/*
    Copyright 2015 Richard S. Tallent, II

    Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files
    (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge,
    publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to
    do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
    LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
    CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

namespace Ci.Sequential
{
    using System;

    /// <summary>
    /// Static methods to set and retrieve the timestamp for a byte-order variant COMB Guid.
    /// </summary>
    public static class Byte
    {

        /// <summary>
        /// Returns a new Guid COMB, consisting of a random Guid combined with the current UTC timestamp.
        /// </summary>
        public static System.Guid Create() => Create(System.Guid.NewGuid(), DateTime.UtcNow);

        /// <summary>
        /// Returns a new Guid COMB, consisting of the specified Guid combined with the current UTC timestamp.
        /// </summary>
        public static System.Guid Create(System.Guid value) => Create(value, DateTime.UtcNow);

        /// <summary>
        /// Returns a new Guid COMB, consisting of a random Guid combined with the provided timestamp.
        /// </summary>
        public static System.Guid Create(DateTime timestamp) => Create(System.Guid.NewGuid(), timestamp);

        /// <summary>
        /// Returns a new Guid COMB, consisting of the provided Guid and provided timestamp.
        /// </summary>
        public static System.Guid Create(System.Guid value, DateTime timestamp)
        {
            var bytes = value.ToByteArray();
            var dtbytes = Utilities.DateTimeToBytes(timestamp);

            // Nybble 6-9 move left to 5-8. Nybble 9 is set to "4" (the version)
            dtbytes[2] = (byte)((byte)(dtbytes[2] << 4) | (byte)(dtbytes[3] >> 4));
            dtbytes[3] = (byte)((byte)(dtbytes[3] << 4) | (byte)(dtbytes[4] >> 4));
            dtbytes[4] = (byte)(0x40 | (byte)(dtbytes[4] & 0x0F));

            // Overwrite the first six bytes
            Array.Copy(dtbytes, 0, bytes, 0, Utilities.NumDateBytes);
            return new System.Guid(bytes);
        }

        /// <summary>
        /// Returns the timestamp previously stored in a COMB Guid value.
        /// </summary>
        public static DateTime GetTimestamp(System.Guid comb)
        {
            var bytes = comb.ToByteArray();
            var dtbytes = new byte[Utilities.NumDateBytes];
            Array.Copy(bytes, 0, dtbytes, 0, Utilities.NumDateBytes);

            // Move nybbles 5-8 to 6-9, overwriting the existing 9 (version "4")
            dtbytes[4] = (byte)((byte)(dtbytes[3] << 4) | (byte)(dtbytes[4] & 0x0F));
            dtbytes[3] = (byte)((byte)(dtbytes[2] << 4) | (byte)(dtbytes[3] >> 4));
            dtbytes[2] = (byte)(dtbytes[2] >> 4);
            return Utilities.BytesToDateTime(dtbytes);
        }

    }

}