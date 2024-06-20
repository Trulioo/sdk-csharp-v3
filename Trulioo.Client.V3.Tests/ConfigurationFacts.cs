using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Trulioo.Client.V3.Tests
{
    public class ConfigurationFacts
    {
        [Fact(Skip = "Calls API")]
        public async Task GetAllDatasourcesTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            {
                var response = await client.Configuration.GetAllDatasourcesAsync(BaseFact.PackageId);
                var datasources = new List<string>();

                foreach (var r in response)
                {
                    datasources.AddRange(r.Datasources.Select(d => d.Name));
                }

                Assert.NotEmpty(response);
                Assert.NotEmpty(datasources);
            }
        }

        [Fact(Skip = "Calls API")]
        public async Task GetConsentsTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            {
                var response = await client.Configuration.GetСonsentsAsync(BaseFact.PackageId, BaseFact.CountryCode);

                Assert.NotNull(response);
            }
        }

        [Fact(Skip = "Calls API")]
        public async Task GetDetailedConsentsTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            {
                var response = await client.Configuration.GetDetailedСonsentsAsync(BaseFact.PackageId, BaseFact.CountryCode);

                Assert.NotNull(response);
            }
        }

        [Fact(Skip = "Calls API")]
        public async Task GetCountryCodesTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            {
                var response = await client.Configuration.GetCountryCodesAsync(BaseFact.PackageId);

                Assert.NotNull(response);
                Assert.NotEmpty(response);
            }
        }

        [Fact(Skip = "Calls API")]
        public async Task GetCountrySubdivisionsTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            {
                var response = await client.Configuration.GetCountrySubdivisionsAsync(BaseFact.CountryCode);

                Assert.NotNull(response);
                Assert.NotEmpty(response);
            }
        }

        [Fact(Skip = "Calls API")]
        public async Task GetFieldsTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            {
                var response = await client.Configuration.GetFieldsAsync(BaseFact.PackageId, BaseFact.CountryCode);

                Assert.NotNull(response);
                Assert.NotEmpty(response);
            }
        }

        [Fact(Skip = "Calls API")]
        public async Task GetRecommendedFieldsTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            {
                var response = await client.Configuration.GetRecommendedFieldsAsync(BaseFact.PackageId, BaseFact.CountryCode);

                Assert.NotNull(response);
                Assert.NotEmpty(response);
            }
        }

        [Fact(Skip = "Calls API")]
        public async Task GetTestEntitiesTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            {
                var response = await client.Configuration.GetTestEntitiesAsync(BaseFact.PackageId, BaseFact.CountryCode);

                Assert.NotNull(response);
                Assert.NotEmpty(response);
            }
        }

        [Fact(Skip = "Calls API")]
        public async Task GetDatasourcesTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            {
                var response = await client.Configuration.GetDatasourcesAsync(BaseFact.PackageId, BaseFact.CountryCode);

                Assert.NotNull(response);
                Assert.NotEmpty(response);
            }
        }
    }
}