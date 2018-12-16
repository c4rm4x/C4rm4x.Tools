using System;
using System.Security.Cryptography;

namespace C4rm4x.Tools.Security.GoogleAuthenticator
{
    internal static class HashExtensions
    {
        public static string GenerateHashedCode(
            this byte[] key, 
            long iterationNumber, 
            int digits = 6)
        {
            var counter = BitConverter.GetBytes(iterationNumber);

            if (BitConverter.IsLittleEndian) Array.Reverse(counter);

            var hash = new HMACSHA1(key, true).ComputeHash(counter);

            var offset = hash[hash.Length - 1] & 0xf;

            // Convert the 4 bytes into an integer, ignoring the sign.
            var binary =
                ((hash[offset] & 0x7f) << 24) |
                (hash[offset + 1] << 16) |
                (hash[offset + 2] << 8) |
                (hash[offset + 3]);

            var password = binary % (int)Math.Pow(10, digits);

            return password.ToString(new string('0', digits));
        }
    }
}
