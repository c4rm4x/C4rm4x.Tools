#region Using

using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

#endregion

namespace C4rm4x.Tools.HttpUtilities.Test
{
    public partial class RESTfulConsumerTest
    {
        [TestClass]
        public class RESTfulConsumerPostTest
        {
            private const string Domain = "http://api.email-validator.net/api/";
            private const string Verify = "verify";

            #region Helper classes

            private class ApiRequest
            {
                public string EmailAddress { get; set; }

                public string APIKey { get; set; }
            }

            #endregion

            [TestMethod, IntegrationTest]
            public void Post_Sends_A_New_Request_To_Domain()
            {
                var request = BuildRequest();

                RESTfulConsumer.Post(request, Domain, Verify);
            }

            private static ApiRequest BuildRequest()
            {
                return new ApiRequest
                {
                    APIKey = ApiKey,
                    EmailAddress = ObjectMother.Create<string>(),
                };
            }

            private static string ApiKey
            {
                get { return ConfigurationManager.AppSettings["ApiKey"]; }
            }
        }
    }
}
