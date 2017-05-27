#region Using

using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace C4rm4x.Tools.HttpUtilities.Test
{
    public partial class RESTfulConsumerTest
    {
        [TestClass]
        public class RESTfulConsumerGetTest
        {
            private const string Domain = "https://api.spotify.com/v1/search";

            #region Helper classes

            public class ExternalUrls
            {
                public string spotify { get; set; }
            }

            public class Followers
            {
                public object href { get; set; }

                public int total { get; set; }
            }

            public class Item
            {
                public ExternalUrls external_urls { get; set; }

                public Followers followers { get; set; }

                public List<object> genres { get; set; }

                public string href { get; set; }

                public string id { get; set; }

                public List<object> images { get; set; }

                public string name { get; set; }

                public int popularity { get; set; }

                public string type { get; set; }

                public string uri { get; set; }
            }

            public class Artists
            {
                public string href { get; set; }

                public List<Item> items { get; set; }

                public int limit { get; set; }

                public string next { get; set; }

                public int offset { get; set; }

                public object previous { get; set; }

                public int total { get; set; }
            }

            public class RootObject
            {
                public Artists artists { get; set; }
            }

            #endregion

            [TestMethod, IntegrationTest]
            public async Task GetAsync_Returns_An_Instance_Of_Specified_Type()
            {
                var result = await RESTfulConsumer.GetAsync<RootObject>(Domain, string.Empty, null,
                    new KeyValuePair<string, object>("q", ObjectMother.Create<string>()),
                    new KeyValuePair<string, object>("type", "artist"));

                Assert.IsNotNull(result);
            }
        }
    }
}
