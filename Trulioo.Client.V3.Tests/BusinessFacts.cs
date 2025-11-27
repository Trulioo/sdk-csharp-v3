using Trulioo.Client.V3.Enums;
using Trulioo.Client.V3.Models.Business;
using Trulioo.Client.V3.Models.Fields;
using Trulioo.Client.V3.Tests.MemberData;
using Xunit;

namespace Trulioo.Client.V3.Tests
{
    public class BusinessFacts
    {
        [Theory(Skip = "Calls API")]
        [MemberData(nameof(KybTestData.BusinessSearchTestData), MemberType = typeof(KybTestData))]
        public async void BusinessSearchTest(BusinessSearchRequest request, BusinessSearchResponse expectedResponse)
        {
            //Arrange
            using var client = await BaseFact.GetTruliooKYBClientAsync();
            var response = await client.TruliooBusiness.BusinessSearchAsync(request);

            Assert.Equal(expectedResponse.Record.RecordStatus, response.Record.RecordStatus);
            Assert.Equal(expectedResponse.CountryCode, response.CountryCode);

            Assert.Equal(expectedResponse.Record.DatasourceResults.Count(), response.Record.DatasourceResults.Count());
            List<string> expectedDatasourcesNames = expectedResponse.Record.DatasourceResults.Select(x => x.DatasourceName).ToList();
            List<string> actualDatasourceNames = response.Record.DatasourceResults.Select(x => x.DatasourceName).ToList();
            Assert.True(expectedDatasourcesNames.All(actualDatasourceNames.Contains));

            List<string> expectedBusinessNameResults = expectedResponse.Record.DatasourceResults.SelectMany(datasource => datasource.Results.Select(result => result.BusinessName)).ToList();
            List<string> actualBusinessNameResults = response.Record.DatasourceResults.SelectMany(datasource => datasource.Results.Select(result => result.BusinessName)).ToList();
            Assert.Equal(expectedBusinessNameResults.Count(), actualBusinessNameResults.Count());
            Assert.True(expectedBusinessNameResults.All(actualBusinessNameResults.Contains));

            List<string> expectedBusinessNumberResults = expectedResponse.Record.DatasourceResults.SelectMany(datasource => datasource.Results.Select(result => result.BusinessRegistrationNumber)).ToList();
            List<string> actualBusinessNumberResults = response.Record.DatasourceResults.SelectMany(datasource => datasource.Results.Select(result => result.BusinessRegistrationNumber)).ToList();
            Assert.Equal(expectedBusinessNumberResults.Count(), actualBusinessNumberResults.Count());
            Assert.True(expectedBusinessNumberResults.All(actualBusinessNumberResults.Contains));
        }

        [Theory(Skip = "Calls API")]
        [MemberData(nameof(BusinessVerifyTestData))]
        public async void BusinessVerifyTest(BusinessVerifyRequest request)
        {
            using var client = await BaseFact.GetTruliooKYBClientAsync();
            var response = await client.TruliooBusiness.BusinessVerifyAsync(request);

            Assert.NotNull(response);
        }

        [Theory(Skip = "Calls API")]
        [InlineData("CA", null)]
        [InlineData("CA", "BC")]
        [InlineData(null, null)]
        [InlineData(" ", "")]
        [InlineData("", "BC")]
        [InlineData(null, "BC")]
        public async Task GetBusinessRegistrationNumbers(string countryCode, string jurisdiction)
        {
            using var client = await BaseFact.GetTruliooKYBClientAsync();

            if (string.IsNullOrWhiteSpace(countryCode) && !string.IsNullOrWhiteSpace(jurisdiction))
            {
                await Assert.ThrowsAsync<ArgumentException>(async () => await client.TruliooBusiness.GetBusinessRegistrationNumbersAsync(countryCode, jurisdiction));
            }
            else
            {
                var response = await client.TruliooBusiness.GetBusinessRegistrationNumbersAsync(countryCode, jurisdiction);
                Assert.NotNull(response);
            }
            
        }

        [Theory(Skip = "Calls API")]
        [InlineData("CA")]
        [InlineData(null)]
        [InlineData(" ")]
        public async Task GetCountryJOI(string countryCode)
        {
            using var client = await BaseFact.GetTruliooKYBClientAsync();
            var response = await client.TruliooBusiness.GetCountryJOIAsync(countryCode);
            Assert.NotNull(response);
        }


        #region Member Data

        public static IEnumerable<object[]> BusinessVerifyTestData()
        {
            yield return new object[]
            {
                new BusinessVerifyRequest
                {
                    PackageId = BaseFact.PackageId,
                    VerificationType = VerificationType.Test,
                    VerboseMode = true,
                    CountryCode = "CA",
                    BusinessDataFields = new BusinessDataFields
                    {
                        BusinessName = "BusinessName",
                        TradestyleName = "TradestyleName",
                        TaxIDNumber = "TaxIDNumber",
                        BusinessRegistrationNumber = "BusinessRegistrationNumber",
                        DayOfIncorporation = 1,
                        MonthOfIncorporation = 2,
                        YearOfIncorporation = 2003,
                        JurisdictionOfIncorporation = "JurisdictionOfIncorporation",
                        ShareholderListDocument = true,
                        FinancialInformationDocument = true,
                        EnhancedProfile = true,
                        DunsNumber = "DunsNumber",
                        Entities = true,
                        PeopleOfSignificantControl = new List<PersonOfSignificantControl>
                        {
                            new PersonOfSignificantControl
                            {
                                FirstGivenName = "FirstGivenName",
                                MiddleName = "MiddleName",
                                FirstSurName = "FirstSurName",
                                SecondSurname = "SecondSurname",
                                FullName = "FullName",
                                BusinessName = "BusinessName",
                                YearOfBirth = "YearOfBirth",
                                MonthOfBirth = "MonthOfBirth",
                                DayOfBirth = "DayOfBirth",
                            }
                        },
                        Filings = true,
                        ArticleOfAssociation = true,
                        RegistrationDetails = true,
                        AnnualReport = true,
                        RegisterReport = true,
                        CreditCheck = true,
                        CreditReport = true,
                        GISAExtract = true,
                        VRExtract = true,
                        RegisterCheck = true,
                        TradeRegisterReport = true,
                        BeneficialOwnersCheck = true,
                        AnnualAccounts = true,
                        FiledChanges = true,
                        FiledDocuments = true,
                        CompletePlus = true,
                        ArticleOfAuthority = true,
                        AgentAddressChange = true,
                        Location = new List<BusinessLocation>
                        {
                            new BusinessLocation
                            {
                                Index = 1,
                                Address1 = "1234 Main Street",
                                Country = "CA"
                            }
                        }
                    }
                },
            };
        }


        #endregion
    }
}
