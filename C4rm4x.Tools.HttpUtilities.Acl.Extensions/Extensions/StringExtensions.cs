#region Using

using C4rm4x.Tools.Utilities;
using System;
using System.Text;

#endregion

namespace C4rm4x.Tools.HttpUtilities.Acl
{
    internal static class StringExtensions
    {
        public static string FromBase64(
            this string base64)
        {
            base64.NotNullOrEmpty(nameof(base64));

            try
            {
                return Encoding.UTF8.GetString(
                    Convert.FromBase64String(base64));
            }
            catch (FormatException)
            {
                return string.Empty;
            }
        }

        public static string AsBase64(
            this string value)
        {
            value.NotNullOrEmpty(nameof(value));

            try
            {
                return Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(value));
            }
            catch (FormatException)
            {
                return string.Empty;
            }
        }
    }
}
