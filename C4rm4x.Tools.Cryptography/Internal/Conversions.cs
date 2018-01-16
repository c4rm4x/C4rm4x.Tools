#region Using

using System;
using System.Text;

#endregion

namespace C4rm4x.Tools.Cryptography
{
    internal static class Conversions
    {
        public static byte[] AsBytes(this string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }

        public static byte[] FromBase64(this string text)
        {
            return Convert.FromBase64String(text);
        }

        public static string AsString(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public static string AsBase64(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
    }
}
