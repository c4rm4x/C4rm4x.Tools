using C4rm4x.Tools.Utilities;
using Newtonsoft.Json;
using System.Text;

namespace C4rm4x.Tools.Security.Acl
{ 
    internal static class ObjectExtensions
    {
        public static byte[] AsBytes<T>(
            this T value)
        {
            value.NotNull(nameof(value));

            return Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(value));
        }
    }
}
