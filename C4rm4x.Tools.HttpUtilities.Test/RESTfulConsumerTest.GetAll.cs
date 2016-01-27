#region Using

using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace C4rm4x.Tools.HttpUtilities.Test
{
    public partial class RESTfulConsumerTest
    {
        [TestClass]
        public class RESTfulConsumerGetAllTest
        {
            private const string Domain = "https://restcountries.eu/rest/v1/";
            private const string All = "all";

            #region Helper classes

            private class Country
            {
                public string Name { get; set; }

                public string Capital { get; set; }

                public List<string> AltSpellings { get; set; }

                public string Relevance { get; set; }

                public string Region { get; set; }

                public string Subregion { get; set; }

                public int Population { get; set; }

                public List<double> Latlng { get; set; }

                public string Demonym { get; set; }

                public double? Area { get; set; }

                public double? Gini { get; set; }

                public List<string> Timezones { get; set; }

                public List<string> Borders { get; set; }

                public string NativeName { get; set; }

                public List<string> CallingCodes { get; set; }

                public List<string> TopLevelDomain { get; set; }

                public string Alpha2Code { get; set; }

                public string Alpha3Code { get; set; }

                public List<string> Currencies { get; set; }

                public List<string> Languages { get; set; }
            }

            #endregion

            [TestMethod, IntegrationTest]
            public void GetAll_Returns_All_The_Instances_Of_Specified_Type()
            {
                var result = RESTfulConsumer.GetAll<Country>(Domain, All);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.Any());
            }
        }
    }
}
