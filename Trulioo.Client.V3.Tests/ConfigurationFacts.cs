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
            using (var client = await BaseFact.GetTruliooClientAsync())
            {
                var response = await client.Configuration.GetAllDatasourcesAsync("packageid");
                var datasources = new List<string>();

                foreach (var r in response)
                {
                    datasources.AddRange(r.Datasources.Select(d => d.Name));
                }

                Assert.NotEmpty(response);
                Assert.NotEmpty(datasources);
            }
        }
    }
}
