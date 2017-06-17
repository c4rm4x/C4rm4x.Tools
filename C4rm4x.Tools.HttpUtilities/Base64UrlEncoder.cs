#region Using

using C4rm4x.Tools.Utilities;
using System;
using System.Text;

#endregion

namespace C4rm4x.Tools.HttpUtilities
{
    /// <summary>
    /// Encodes and decodes strings as Base64 URL safe encoding
    /// </summary>
    public static class Base64UrlEncoder
    {
        /// <summary>
        /// The following functions perform base64url encoding which differs from regular base64 encoding as follows
        /// * padding is skipped so the pad character '=' doesn't have to be percent encoded
        /// * the 62nd and 63rd regular base64 encoding characters ('+' and '/') are replace with ('-' and '_')
        /// The changes make the encoding alphabet file and URL safe.
        /// </summary>
        /// <param name="str">string to encode.</param>
        /// <returns>Base64 URL safe encoding of the UTF8 bytes.</returns>
        public static string Encode(string str)
        {
            str.NotNullOrEmpty(nameof(str));

            return Encode(Encoding.UTF8.GetBytes(str));
        }


        private static string Encode(byte[] inArray)
        {
            return Convert
                .ToBase64String(inArray, 0, inArray.Length)
                .Split('=')[0]      // Remove any trailing padding
                .Replace('+', '-')  // 62nd char of encoding
                .Replace('/', '_'); // 63rd char of encoding
        }

        /// <summary>
        /// Decodes the string from Base64UrlEncoded to UTF8.
        /// </summary>
        /// <param name="str">string to decode.</param>
        /// <returns>UTF8 string.</returns>
        public static string Decode(string str)
        {
            return Encoding.UTF8.GetString(DecodeBytes(str));
        }

        private static byte[] DecodeBytes(string str)
        {
            str.NotNullOrEmpty(nameof(str));
            
            str = str
                .Replace('-', '+')  // 62nd char of encoding
                .Replace('_', '/'); // 63rd char of encoding

            // check for padding
            switch (str.Length % 4)
            {
                case 0: break;

                case 2:
                    str += "==";
                    break;

                case 3:
                    str += "=";
                    break;

                default: throw new ArgumentException("Invalid value");
            }

            return Convert.FromBase64String(str);
        }
    }
}
