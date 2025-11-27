using Trulioo.Client.V3.Enums;
using Trulioo.Client.V3.Models.Fields;
using Trulioo.Client.V3.Models.LocationFields;
using Trulioo.Client.V3.Models.Risk;
using Xunit;

namespace Trulioo.Client.V3.Tests
{
    public class PersonFraudFacts
    {
        [Theory(Skip = "Calls API")]
        [MemberData(nameof(RiskVerifyTestData))]
        public async void PersonFraudCheckTest(RiskVerifyRequest request)
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.PersonFraud.PersonFraudCheckAsync(request);

            Assert.NotNull(response.TransactionId);
            Assert.NotNull(response.Record);
            Assert.NotNull(response.Record.TransactionRecordId);
        }

        [Fact(Skip = "Calls API")]
        public async Task PersonFraudGetDatasourcesTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.PersonFraud.GetDatasourcesAsync(BaseFact.PackageId, BaseFact.CountryCode);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task PersonFraudGetTestEntitiesTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.PersonFraud.GetTestEntitiesAsync(BaseFact.PackageId, BaseFact.CountryCode);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task PersonFraudGetCountrySubdivisionsTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.PersonFraud.GetCountrySubdivisionsAsync(BaseFact.CountryCode);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task PersonFraudGetCountryCodesTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.PersonFraud.GetCountryCodesAsync(BaseFact.PackageId);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task PersonFraudGetFieldsTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.PersonFraud.GetFieldsAsync(BaseFact.PackageId, BaseFact.CountryCode);

            Assert.NotNull(response);
            Assert.NotEmpty(response);

        }

        [Fact(Skip = "Calls API")]
        public async Task PersonFraudGetRecommendedFieldsTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.PersonFraud.GetRecommendedFieldsAsync(BaseFact.PackageId, BaseFact.CountryCode);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task PersonFraudGetDetailedConsentsTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.PersonFraud.GetDetailedConsentsAsync(BaseFact.PackageId, BaseFact.CountryCode);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task PersonFraudGetConsentsTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.PersonFraud.GetConsentsAsync(BaseFact.PackageId, BaseFact.CountryCode);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        public static IEnumerable<object[]> RiskVerifyTestData()
        {
            yield return new object[]
            {
                new RiskVerifyRequest
                {
                    PackageId = BaseFact.PackageId,
                    VerificationType = VerificationType.Test,
                    CountryCode = "",
                    DataFields = new DataFields
                    {
                        PersonInfo = new PersonInfo
                        {
                            FirstGivenName = "",
                            FirstSurName = "",
                            YearOfBirth = 1990
                        },
                        Location = new Location
                        {
                            AdditionalFields = new AdditionalFields
                            {
                                Address1 = ""
                            }
                        },
                        Communication = new Communication { 
                            Telephone = "",
                            EmailAddress = ""
                        },
                        Risk = new RiskMonitorSettings
                        {
                            Action = ""
                        },
                        CountrySpecific = new CountrySpecific
                        {
                            [""] = new Dictionary<string, string>
                            {
                                [""] = ""
                            }
                        }
                    },
                    VerboseMode = true
                }
            };
        }
    }
}
