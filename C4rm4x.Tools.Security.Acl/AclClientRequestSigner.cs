using C4rm4x.Tools.Utilities;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace C4rm4x.Tools.Security.Acl
{
    /// <summary>
    /// Acl request signer is responsible to generate a
    /// base-64 signature for the given object
    /// </summary>
    public class AclClientRequestSigner
    {
        /// <summary>
        /// Generates a valid base-64 signature for the given object
        /// </summary>
        /// <param name="objectToSign">The object to sign</param>
        /// <param name="secretAsBase64">The secret (as a base-64 string)</param>
        /// <returns>The base-64 signature</returns>
        public string Sign<T>(T objectToSign, string secretAsBase64)
        {
            objectToSign.NotNull(nameof(objectToSign));
            secretAsBase64.NotNullOrEmpty(nameof(secretAsBase64));

            return Sign(objectToSign.AsBytes(), secretAsBase64);
        }

        /// <summary>
        /// Generates a valid base-64 signature for the given body
        /// </summary>
        /// <param name="body">The body to sign</param>
        /// <param name="secretAsBase64">The secret (as a base-64 string)</param>
        /// <returns>The base-64 signature</returns>
        public string Sign(byte[] body, string secretAsBase64)
        {
            body.NotNull(nameof(body));
            secretAsBase64.NotNullOrEmpty(nameof(secretAsBase64));

            return Sign(body, Convert.FromBase64String(secretAsBase64));
        }

        private static string Sign(byte[] body, byte[] secret)
        {
            using (var hmac = new HMACSHA256(secret))
            {
                var hash = hmac.ComputeHash(body);

                return string
                    .Join(string.Empty, hash.Select(b => b.ToString("x2")))
                    .AsBase64();
            }
        }
    }
}
