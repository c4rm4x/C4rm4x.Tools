namespace C4rm4x.Tools.Security.GoogleAuthenticator
{
    /// <summary>
    /// Setup result (including alternative code)
    /// </summary>
    public class SetupResult
    {
        /// <summary>
        /// Alternative code when camera is disabled
        /// </summary>
        public string ManualEntryKey { get; internal set; }

        /// <summary>
        /// QrCode url
        /// </summary>
        public string QrCodeImageUrl { get; internal set; }
    }
}
