#region Using

using C4rm4x.Tools.Utilities;
using Newtonsoft.Json;
using System.Security.Cryptography;

#endregion

namespace C4rm4x.Tools.Cryptography
{
    /// <summary>
    /// Tools for encryption/decription
    /// </summary>
    public static class CryptoExtensions
    {
        /// <summary>
        /// Encrypts the content of the object serialized as json
        /// </summary>
        /// <typeparam name="T">Type of the object to encrypt</typeparam>
        /// <param name="obj">The object to encrypt</param>
        /// <param name="key">The key</param>
        /// <returns>The object encrypted with the given key as base-64 equivalent string</returns>
        public static string EncryptObject<T>(this T obj, string key)
        {
            obj.NotNull(nameof(obj));

            return JsonConvert.SerializeObject(obj).Encrypt(key);
        }

        /// <summary>
        /// Encrypts the given string
        /// </summary>
        /// <param name="text">The text to encrypt</param>
        /// <param name="key">The key</param>
        /// <returns>The text encrypted with the given key as base-64</returns>
        public static string Encrypt(this string text, string key)
        {
            text.NotNullOrEmpty(nameof(text));
            key.NotNullOrEmpty(nameof(key));

            using (var rsa = new RSACryptoServiceProvider())
            {
                try
                {                    
                    rsa.FromXmlString(key);

                    var encryptedData = rsa.Encrypt(text.AsBytes(), true);

                    return encryptedData.AsBase64();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        /// <summary>
        /// Decrypts the given string
        /// </summary>
        /// <param name="text">The text to decrypt</param>
        /// <param name="key">The key</param>
        /// <returns>The text decrypted with the given key</returns>
        public static string Decrypt(this string text, string key)
        {
            text.NotNullOrEmpty(nameof(text));
            key.NotNullOrEmpty(nameof(key));

            using (var rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    rsa.FromXmlString(key);

                    var resultBytes = text.FromBase64();

                    var decryptedBytes = rsa.Decrypt(resultBytes, true);

                    return decryptedBytes.AsString();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        /// <summary>
        /// Decrypts the given string and parse as instance of type T
        /// </summary>
        /// <typeparam name="T">Type of object to return</typeparam>
        /// <param name="text">The text to decrypt</param>
        /// <param name="key">The key</param>
        /// <returns>An instance of type T based on the encrypted text</returns>
        public static T DecryptObject<T>(this string text, string key)
        {
            return JsonConvert.DeserializeObject<T>(text.Decrypt(key));
        }        
    }
}
