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
            AddItemName = "subscription",
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
        /// <returns>The element for the given index</returns>
        public SubscriptionConfigurationElement this[int index]
        {
            get { return (SubscriptionConfigurationElement)BaseGet(index); }
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
        /// <returns>The element for the given name</returns>
        public new SubscriptionConfigurationElement this[string name]
        {
            get { return (SubscriptionConfigurationElement)BaseGet(name); }
            set
            {
                if (BaseGet(name).IsNotNull())
                    Remove(name);

                Add(value);
            }
        }

        /// <summary>
        /// Adds a subscription configuration element to the System.Configuration.ConfigurationElementCollection
        /// </summary>
        /// <param name="subscriptionConfig">The subscription config to be added</param>
        public void Add(SubscriptionConfigurationElement subscriptionConfig)
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
        public void Remove(SubscriptionConfigurationElement subscriptionConfig)
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
        /// <returns>The element key</returns>
        protected override object GetElementKey(
            ConfigurationElement element)
        {
            return (element as SubscriptionConfigurationElement).Name;
        }

        /// <summary>
        /// Creates a new subscription config element
        /// </summary>
        /// <returns>A new element</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new SubscriptionConfigurationElement();
        }

        /// <summary>
        /// Gets the name used to identify this collection of elements in the configuration file when overridden in a derived class
        /// </summary>
        protected override string ElementName
        {
            get { return "subscription"; }
        }
    }

    /// <summary>
    /// The subscription configuration for this client
    /// </summary>
    public class SubscriptionConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SubscriptionConfigurationElement()
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Subscription identifier</param>
        /// <param name="baseApiUrl">Base API url to be consumed</param>
        /// <param name="username">The subscriber identifier in the API</param>
        /// <param name="password">The shared secret between client and API (in base 64 format)</param>
        /// <param name="digitalSignature">The header to use to sign POST/PUT requests (if any)</param>
        public SubscriptionConfigurationElement(
            string name,
            string baseApiUrl,
            string username,
            string password,
            DigitalSignatureConfigurationElement digitalSignature = null)
        {
            name.NotNullOrEmpty(nameof(name));
            baseApiUrl.NotNullOrEmpty(nameof(baseApiUrl));
            username.NotNullOrEmpty(nameof(username));
            password.NotNullOrEmpty(nameof(password));

            Name = name;
            BaseApiUrl = baseApiUrl;
            Username = username;
            Password = password;
            DigitalSignature = digitalSignature;
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
        [ConfigurationProperty("username", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Username
        {
            get { return (string)this["username"]; }
            set { this["username"] = value; }
        }

        /// <summary>
        /// Gets or sets the password (in base 64 format)
        /// </summary>
        [ConfigurationProperty("password", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Password
        {
            get { return (string)this["password"]; }
            set { this["password"] = value; }
        }

        /// <summary>
        /// Gets or sets the header to use to sign POST/PUT requests (if present)
        /// </summary>
        [ConfigurationProperty("digitalSignature", DefaultValue = null, IsKey = false, IsRequired = false)]
        public DigitalSignatureConfigurationElement DigitalSignature
        {
            get { return (DigitalSignatureConfigurationElement)this["digitalSignature"]; }
            set { this["digitalSignature"] = value; }
        }
    }

    /// <summary>
    /// The digital signature configuration for this client (if any)
    /// </summary>
    public class DigitalSignatureConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DigitalSignatureConfigurationElement()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="header">The header name</param>
        /// <param name="sharedSecret">Shared secret between client and server</param>
        public DigitalSignatureConfigurationElement(
            string header,
            string sharedSecret)
        {
            header.NotNullOrEmpty(nameof(header));
            sharedSecret.NotNullOrEmpty(nameof(sharedSecret));

            Header = header;
            SharedSecret = sharedSecret;
        }

        /// <summary>
        /// Gets or sets the subscriber digital signature header
        /// </summary>
        [ConfigurationProperty("header", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Header
        {
            get { return (string)this["header"]; }
            set { this["header"] = value; }
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

        public bool IsEmpty => Header.IsNullOrEmpty() && SharedSecret.IsNullOrEmpty();
    }
}