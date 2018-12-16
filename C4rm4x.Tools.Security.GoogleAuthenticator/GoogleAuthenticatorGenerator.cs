using C4rm4x.Tools.Utilities;
using System.Text;

namespace C4rm4x.Tools.Security.GoogleAuthenticator
{
    /// <summary>
    /// Service responsible to generate QrCodes / alternative authentication code
    /// </summary>
    public static class GoogleAuthenticatorGenerator
    {
        /// <summary>
        /// Generates a setup code for a Google Authenticator user to scan (with issuer ID).
        /// </summary>
        /// <param name="issuer">Issuer ID (the name of the system, i.e. 'MyApp')</param>
        /// <param name="accountTitleNoSpaces">Account Title (no spaces)</param>
        /// <param name="accountSecretKey">Account Secret Key</param>
        /// <param name="qrCodeWidth">QR Code Width</param>
        /// <param name="qrCodeHeight">QR Code Height</param>
        /// <returns>Instance of SetupResult including QrCode and alternative authentication code</returns>
        public static SetupResult Generate(
            string issuer, 
            string accountTitleNoSpaces, 
            string accountSecretKey, 
            int qrCodeWidth= 300, 
            int qrCodeHeight = 300)
        {
            issuer.NotNullOrEmpty(nameof(issuer));
            accountTitleNoSpaces.NotNullOrEmpty(nameof(accountTitleNoSpaces));
            accountSecretKey.NotNullOrEmpty(nameof(accountSecretKey));

            var encodedSecretKey = EncodeAccountSecretKey(accountSecretKey);

            var provisionUrl = "otpauth://totp/{0}?secret={1}&issuer={2}"
                .AsFormat(accountTitleNoSpaces.Replace(" ", string.Empty), encodedSecretKey, issuer.UrlEncode())
                .UrlEncode();

            var url = "https://chart.googleapis.com/chart?cht=qr&chs={0}x{1}&chl={2}"
                .AsFormat(qrCodeWidth, qrCodeHeight, provisionUrl);

            return new SetupResult
            {
                ManualEntryKey = encodedSecretKey,
                QrCodeImageUrl = url
            };
        }

        private static string EncodeAccountSecretKey(string accountSecretKey)
        {
            return Encoding.UTF8.GetBytes(accountSecretKey).AsBase32();
        }
    }
}
