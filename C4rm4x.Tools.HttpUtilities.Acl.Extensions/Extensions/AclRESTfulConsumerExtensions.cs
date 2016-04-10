#region Using

using C4rm4x.Tools.HttpUtilities.Acl.Configuration;
using C4rm4x.Tools.Utilities;
using System;
using System.Configuration;

#endregion

namespace C4rm4x.Tools.HttpUtilities.Acl
{
    internal static class AclRESTfulConsumerExtensions
    {
        private const string AclSubscriptionsSection = "SubscriptionsConfiguration";

        public static AclRESTfulConsumerConfiguration GetClientConfiguration(
            this AclRESTfulConsumer consumer,
            string name)
        {
            var thisSubscriptionConfig = GetSubscriptionConfig(name);

            return new AclRESTfulConsumerConfiguration(
                thisSubscriptionConfig.BaseApiUrl,
                thisSubscriptionConfig.SubscriberIdentifier,
                thisSubscriptionConfig.SharedSecret);
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

        public static string GetAuthorizationHeader(
            this AclRESTfulConsumer consumer,
            AclRESTfulConsumerConfiguration config)
        {
            return "Basic {0}".AsFormat(
                "{0}:{1}".AsFormat(
                    config.SubscriberIdentifier,
                    config.Secret).AsBase64());
        }
    }
}
