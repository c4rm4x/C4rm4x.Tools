#region Using

using C4rm4x.Tools.Utilities;

#endregion

namespace C4rm4x.Tools.HttpUtilities.Acl.Configuration
{
    internal class AclRESTfulConsumerConfiguration
    {
        public string ApiBaseUrl { get; private set; }

        public string SubscriberIdentifier { get; private set; }

        public string SecretAsBase64 { get; private set; }

        public AclRESTfulConsumerConfiguration(
            string apiBaseUrl,
            string subscriberIdentifier,
            string secretAsBase64)
        {
            apiBaseUrl.NotNullOrEmpty(nameof(apiBaseUrl));
            subscriberIdentifier.NotNullOrEmpty(nameof(subscriberIdentifier));
            secretAsBase64.NotNullOrEmpty(nameof(secretAsBase64));

            ApiBaseUrl = apiBaseUrl;
            SubscriberIdentifier = subscriberIdentifier;
            SecretAsBase64 = secretAsBase64;
        }
    }
}
