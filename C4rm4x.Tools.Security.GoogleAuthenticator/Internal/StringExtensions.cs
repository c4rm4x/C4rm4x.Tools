using System.Text;

namespace C4rm4x.Tools.Security.GoogleAuthenticator
{
    internal static class StringExtensions
    {
        public static string AsBase32(this byte[] data)
        {
            const int InByteSize = 8;
            const int OutByteSize = 5;
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567".ToCharArray();

            int i = 0, index = 0, digit = 0;
            int current = 0, next = 0;

            var result = new StringBuilder((data.Length + 7) * InByteSize / OutByteSize);

            while (i < data.Length)
            {
                current = (data[i] >= 0) ? data[i] : (data[i] + 256); // Unsign

                /* Is the current digit going to span a byte boundary? */
                if (index > (InByteSize - OutByteSize))
                {
                    if ((i + 1) < data.Length)
                        next = (data[i + 1] >= 0) ? data[i + 1] : (data[i + 1] + 256);
                    else
                        next = 0;

                    digit = current & (0xFF >> index);
                    index = (index + OutByteSize) % InByteSize;
                    digit <<= index;
                    digit |= next >> (InByteSize - index);
                    i++;
                }
                else
                {
                    digit = (current >> (InByteSize - (index + OutByteSize))) & 0x1F;
                    index = (index + OutByteSize) % InByteSize;
                    if (index == 0) i++;
                }

                result.Append(alphabet[digit]);
            }

            return result.ToString();
        }

        public static string UrlEncode(this string value)
        {
            const string  ValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

            var result = new StringBuilder();

            foreach (var symbol in value)
            {
                if (ValidChars.IndexOf(symbol) != -1)
                    result.Append(symbol);
                else
                    result.Append('%' + string.Format("{0:X2}", (int)symbol));
            }

            return result.ToString().Replace(" ", "%20");
        }
    }
}
