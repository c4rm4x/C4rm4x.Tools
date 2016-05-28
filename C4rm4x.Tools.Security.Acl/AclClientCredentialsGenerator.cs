#region Using

using C4rm4x.Tools.Utilities;

#endregion

namespace C4rm4x.Tools.Security.Acl
{
    /// <summary>
    /// Acl credentials generator is responsible to generate a 
    /// base-64 basic authorization token
    /// </summary>
    public class AclClientCredentialsGenerator
    {
        /// <summary>
        /// Generates a valid base-64 basic authorization token
        /// </summary>
        /// <param name="identifier">The user identifier</param>
        /// <param name="secretAsBase64">The secret (as a base-64 string)</param>
        /// <returns>The base-64 basic authorization token</returns>
        public string Generate(
            string identifier,
            string secretAsBase64)
        {
            identifier.NotNullOrEmpty(nameof(identifier));
            secretAsBase64.NotNullOrEmpty(nameof(secretAsBase64));

            return Generate(
                new AclClientCredentials(
                    identifier,
                    secretAsBase64.FromBase64()));          
        }

        /// <summary>
        /// Generates a valid base-64 basic authorization token
        /// </summary>
        /// <param name="credentials">The credentials (with secret as clear text)</param>
        /// <returns></returns>
        public string Generate(
            AclClientCredentials credentials)
        {
            credentials.NotNull(nameof(credentials));

            return "Basic {0}".AsFormat(
                GetBasicAuthorization(
                    credentials.Identifier,
                    credentials.Secret));
        }

        private string GetBasicAuthorization(
            string identifier, 
            object clearSecret)
        {
            return "{0}:{1}"
                .AsFormat(identifier, clearSecret)
                .AsBase64();
        }
    }
}
