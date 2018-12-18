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
        private const string AclSubscriptionsSection = "subscriptionsConfiguration";

        public static AclRESTfulConsumerConfiguration GetClientConfiguration(
            this AclRESTfulConsumer consumer)
        {
            var thisSubscriptionConfig = GetSubscriptionConfig(consumer.Name);

            return new AclRESTfulConsumerConfiguration(
                thisSubscriptionConfig.BaseApiUrl,
                thisSubscriptionConfig.SubscriberIdentifier,
                thisSubscriptionConfig.SharedSecret,
                thisSubscriptionConfig.SignatureHeader);
        }

        private static SubscriptionConfig GetSubscriptionConfig(
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

        public static Action<HttpRequestHeaders> GetAclHeaders<T>(
            this AclRESTfulConsumer consumer,
            AclRESTfulConsumerConfiguration config,
            T objectToSend)
        {
            return headers =>
            {
                consumer.GetAuthorizationHeader(config);

                if (!config.SignatureHeader.IsNullOrEmpty())
                    consumer.GetSignatureHeader(config, objectToSend);
            };
        }

        public static Action<HttpRequestHeaders> GetAuthorizationHeader(
            this AclRESTfulConsumer consumer,
            AclRESTfulConsumerConfiguration config)
        {
            return RequestHeaderFactory.AddAuthorization(
                new AclClientCredentialsGenerator()
                    .Generate(config.SubscriberIdentifier, config.SecretAsBase64));
        }

        public static Action<HttpRequestHeaders> GetSignatureHeader<T>(
            this AclRESTfulConsumer consumer,
            AclRESTfulConsumerConfiguration config,
            T objectToSend)
        {
            return headers => headers.Add(config.SignatureHeader,
                new AclClientRequestSigner()
                    .Sign(objectToSend, config.SecretAsBase64));
        }
    }
}
