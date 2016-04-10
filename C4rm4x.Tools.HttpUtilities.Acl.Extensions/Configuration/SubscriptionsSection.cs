#region Using

using C4rm4x.Tools.Utilities;
using System.Configuration;

#endregion

namespace C4rm4x.Tools.HttpUtilities.Acl.Configuration
{
    /// <summary>
    /// Custom configuration for all subscriptions of a particular client
    /// </summary>
    public class SubscriptionsSection : 
        ConfigurationSection
    {
        /// <summary>
        /// Gets the collection of all the subscriptions
        /// </summary>
        [ConfigurationProperty("subscriptions", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(SubscriptionCollection),
            AddItemName = "add",
            ClearItemsName = "clear")]
        public SubscriptionCollection Subscriptions
        {
            get { return (SubscriptionCollection)base["subscriptions"]; }
        }
    }

    /// <summary>
    /// The collection of all subscriptions for this client
    /// </summary>
    public class SubscriptionCollection :
        ConfigurationElementCollection
    {
        /// <summary>
        /// Gets or sets a subscription config based on its location in the collection
        /// </summary>
        /// <param name="index">The location of the subscription config in the collection</param>
        /// <returns></returns>
        public SubscriptionConfig this[int index]
        {
            get { return (SubscriptionConfig)BaseGet(index); }
            set
            {
                if (BaseGet(index).IsNotNull())
                    RemoveAt(index);

                BaseAdd(index, value);
            }
        }

        /// <summary>
        /// Gets or sets a subscription config based on its name
        /// </summary>
        /// <param name="name">The subscription config name</param>
        /// <returns></returns>
        public new SubscriptionConfig this[string name]
        {
            get { return (SubscriptionConfig)BaseGet(name); }
            set
            {
                if (BaseGet(name).IsNotNull())
                    Remove(name);

                Add(value);
            }
        }

        /// <summary>
        /// Adds a subscription config element to the System.Configuration.ConfigurationElementCollection
        /// </summary>
        /// <param name="subscriptionConfig">The subscription config to be added</param>
        public void Add(SubscriptionConfig subscriptionConfig)
        {
            BaseAdd(subscriptionConfig);
        }

        /// <summary>
        /// Removes all subscription config elements from the collection
        /// </summary>
        public void Clear()
        {
            BaseClear();
        }

        /// <summary>
        /// Removes a subscription config element from the collection
        /// </summary>
        /// <param name="subscriptionConfig">The subscription config to be deleted</param>
        public void Remove(SubscriptionConfig subscriptionConfig)
        {
            BaseRemove(subscriptionConfig.Name);
        }

        /// <summary>
        /// Removes the subscription config element at the specified index location
        /// </summary>
        /// <param name="index">The location</param>
        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        /// <summary>
        /// Removes a subscription config element from the collection based on its name
        /// </summary>
        /// <param name="name">The subscription name</param>
        public void Remove(string name)
        {
            BaseRemove(name);
        }

        /// <summary>
        /// Gets the subscription config name for an specified subscription config element
        /// </summary>
        /// <param name="element">The element</param>
        /// <returns></returns>
        protected override object GetElementKey(
            ConfigurationElement element)
        {
            return (element as SubscriptionConfig).Name;
        }

        /// <summary>
        /// Creates a new subscription config element
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new SubscriptionConfig();
        }
    }

    /// <summary>
    /// The subscription configuration for this client
    /// </summary>
    public class SubscriptionConfig : ConfigurationElement
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SubscriptionConfig()
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Subscription identifier</param>
        /// <param name="baseApiUrl">Base API url to be consumed</param>
        /// <param name="subscriberIdentifier">The subscriber identifier in the API</param>
        /// <param name="sharedSecret">The shared secret between client and API (in base 64 format)</param>
        public SubscriptionConfig(
            string name,
            string baseApiUrl,
            string subscriberIdentifier,
            string sharedSecret)
        {
            name.NotNullOrEmpty(nameof(name));
            baseApiUrl.NotNullOrEmpty(nameof(baseApiUrl));
            subscriberIdentifier.NotNullOrEmpty(nameof(subscriberIdentifier));
            sharedSecret.NotNullOrEmpty(nameof(sharedSecret));

            Name = name;
            BaseApiUrl = baseApiUrl;
            SubscriberIdentifier = subscriberIdentifier;
            SharedSecret = sharedSecret;
        }

        /// <summary>
        /// Gets or sets this subscription config within the collection
        /// </summary>
        [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Gets or sets the base API url to be consumed
        /// </summary>
        [ConfigurationProperty("baseApiUrl", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string BaseApiUrl
        {
            get { return (string)this["baseApiUrl"]; }
            set { this["baseApiUrl"] = value; }
        }

        /// <summary>
        /// Gets or sets the subscriber identifier in the API
        /// </summary>
        [ConfigurationProperty("subscriberIdentifier", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string SubscriberIdentifier
        {
            get { return (string)this["subscriberIdentifier"]; }
            set { this["subscriberIdentifier"] = value; }
        }

        /// <summary>
        /// Gets or sets the shared secret between client and API (in base 64 format)
        /// </summary>
        [ConfigurationProperty("sharedSecret", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string SharedSecret
        {
            get { return (string)this["sharedSecret"]; }
            set { this["sharedSecret"] = value; }
        }
    }
}