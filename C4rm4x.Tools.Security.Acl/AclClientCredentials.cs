#region Using

using C4rm4x.Tools.Utilities;

#endregion

namespace C4rm4x.Tools.Security.Acl
{
    /// <summary>
    /// Exchange coin for all this project operations
    /// </summary>
    public class AclClientCredentials
    {
        /// <summary>
        /// Gets the subscriber identifier
        /// </summary>
        public string Identifier { get; private set; }

        /// <summary>
        /// Gets the shared secret
        /// </summary>
        public string Secret { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="identifier">The identifier</param>
        /// <param name="secret">The secret</param>
        public AclClientCredentials(
            string identifier,
            string secret)
        {
            identifier.NotNullOrEmpty(nameof(identifier));
            secret.NotNullOrEmpty(nameof(secret));

            Identifier = identifier;
            Secret = secret;
        }
    }
}
