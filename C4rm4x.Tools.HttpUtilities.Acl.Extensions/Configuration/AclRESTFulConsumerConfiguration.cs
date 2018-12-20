#region Using

using C4rm4x.Tools.Utilities;

#endregion

namespace C4rm4x.Tools.HttpUtilities.Acl.Configuration
{
    internal class AclRESTfulConsumerConfiguration
    {
        public string ApiBaseUrl { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public DigitalSignatureConfiguration SignatureConfiguration { get; private set; }

        public AclRESTfulConsumerConfiguration(
            string apiBaseUrl,
            string username,
            string password,
            DigitalSignatureConfiguration signatureConfiguration = null)
        {
            apiBaseUrl.NotNullOrEmpty(nameof(apiBaseUrl));
            username.NotNullOrEmpty(nameof(username));
            password.NotNullOrEmpty(nameof(password));

            ApiBaseUrl = apiBaseUrl;
            Username = username;
            Password = password;
            SignatureConfiguration = signatureConfiguration;
        }
    }
}
