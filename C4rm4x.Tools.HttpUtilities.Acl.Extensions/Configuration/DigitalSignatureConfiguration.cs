using C4rm4x.Tools.Utilities;

namespace C4rm4x.Tools.HttpUtilities.Acl.Configuration
{
    internal class DigitalSignatureConfiguration
    {
        public string Header { get; private set; }

        public string SharedSecret { get; private set; }

        public DigitalSignatureConfiguration(
            string header,
            string sharedSecret)
        {
            header.NotNullOrEmpty(nameof(header));
            sharedSecret.NotNullOrEmpty(nameof(sharedSecret));

            Header = header;
            SharedSecret = sharedSecret;
        }
    }
}
