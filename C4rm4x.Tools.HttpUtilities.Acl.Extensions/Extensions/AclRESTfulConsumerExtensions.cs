#region Using

using C4rm4x.Tools.HttpUtilities.Acl.Configuration;
using C4rm4x.Tools.Security.Acl;
using C4rm4x.Tools.Utilities;
using System;
using System.Configuration;
using System.Net.Http.Headers;

#endregion

namespace C4rm4x.Tools.HttpUtilities.Acl
{
    internal static class AclRESTfulConsumerExtensions
    {
        private const string AclSubscriptionsSection = "subscriptionsSection";

        public static AclRESTfulConsumerConfiguration GetClientConfiguration(
            this AclRESTfulConsumer consumer)
        {
            var thisSubscriptionConfig = GetSubscriptionConfig(consumer.Name);

            return new AclRESTfulConsumerConfiguration(
                thisSubscriptionConfig.BaseApiUrl,
                thisSubscriptionConfig.Username,
                thisSubscriptionConfig.Password,
                thisSubscriptionConfig.DigitalSignature.IsEmpty
                    ? null
                    : new DigitalSignatureConfiguration(
                        thisSubscriptionConfig.DigitalSignature.Header,
                        thisSubscriptionConfig.DigitalSignature.SharedSecret));
        }

        private static SubscriptionConfigurationElement GetSubscriptionConfig(
            string subscriptionName)
        {
            var thisSubscription =
                GetSubscriptionsConfiguration()
                    .Subscriptions[subscriptionName];

            if (thisSubscription.IsNull())
                throw new ArgumentException(
                    "No subscription with name {0} has been found in section {1}"
                        .AsFormat(subscriptionName, AclSubscriptionsSection));

            return thisSubscription;
        }

        private static SubscriptionsSection GetSubscriptionsConfiguration()
        {
            var section =
                ConfigurationManager.GetSection(AclSubscriptionsSection)
                as SubscriptionsSection;

            if (section.IsNull())
                throw new ConfigurationErrorsException(
                    "There is no section {0} in the config file"
                        .AsFormat(AclSubscriptionsSection));

            return section;
        }

        public static Action<HttpRequestHeaders> GetAuthorizationHeader(
            this AclRESTfulConsumer consumer,
            AclRESTfulConsumerConfiguration config)
        {
            return RequestHeaderFactory.AddAuthorization(
                new AclClientCredentialsGenerator()
                    .Generate(config.Username, config.Password));
        }

        public static Action<HttpRequestHeaders> GetAllHeaders<T>(
            this AclRESTfulConsumer consumer,
            AclRESTfulConsumerConfiguration config,
            T objectToSend)
        {
            return headers =>
            {
                consumer.GetAuthorizationHeader(config)(headers);

                if (config.SignatureConfiguration.IsNotNull())
                    consumer.GetSignatureHeader(config, objectToSend)(headers);
            };
        }

        public static Action<HttpRequestHeaders> GetSignatureHeader<T>(
            this AclRESTfulConsumer consumer,
            AclRESTfulConsumerConfiguration config,
            T objectToSend)
        {
            var signatureConfiguration = config.SignatureConfiguration;

            return headers => headers.Add(signatureConfiguration.Header,
                new AclClientRequestSigner()
                    .Sign(objectToSend, signatureConfiguration.SharedSecret));
        }
    }
}
