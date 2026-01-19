using Trulioo.Client.V3.Enums;
using Trulioo.Client.V3.Models.Fields;
using Trulioo.Client.V3.Models.Verification;
using Xunit;

namespace Trulioo.Client.V3.Tests
{
    public class WatchlistFacts
    {

        [Theory(Skip = "Calls API")]
        [MemberData(nameof(VerifyTestData))]
        public async Task WatchlistVerifyTest(VerifyRequest request)
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Watchlist.VerifyAsync(request);

            Assert.NotNull(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task WatchlistGetAccountPackagesTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Watchlist.GetAccountPackagesAsync();

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task WatchlistGetDetailedConsentsTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Watchlist.GetDetailedConsentsAsync(BaseFact.PackageId, BaseFact.CountryCode);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task WatchlistGetConsentsTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Watchlist.GetConsentsAsync(BaseFact.PackageId, BaseFact.CountryCode);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task WatchlistGetCountryCodesTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Watchlist.GetCountryCodesAsync(BaseFact.PackageId);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task WatchlistGetCountrySubdivisionsTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Watchlist.GetCountrySubdivisionsAsync(BaseFact.CountryCode);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task WatchlistGetFieldsTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Watchlist.GetFieldsAsync(BaseFact.PackageId, BaseFact.CountryCode);

            Assert.NotNull(response);
            Assert.NotEmpty(response);

        }

        [Fact(Skip = "Calls API")]
        public async Task WatchlistGetRecommendedFieldsTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Watchlist.GetRecommendedFieldsAsync(BaseFact.PackageId, BaseFact.CountryCode);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task WatchlistGetTestEntitiesTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Watchlist.GetTestEntitiesAsync(BaseFact.PackageId, BaseFact.CountryCode);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task WatchlistGetDatasourcesTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Watchlist.GetDatasourcesAsync(BaseFact.PackageId, BaseFact.CountryCode);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task WatchlistSayHelloTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Watchlist.SayHelloAsync("");

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task WatchlistTestAuthenticationTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Watchlist.TestAuthenticationAsync();

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Theory(Skip = "Calls API")]
        [MemberData(nameof(TransactionStatusTestData))]
        public async Task WatchlistAsyncCallbackTest(TransactionStatus transactionStatus)
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Watchlist.AsyncCallbackAsync(transactionStatus);

            Assert.NotNull(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task WatchlistGetTransactionRecordTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Watchlist.GetTransactionRecordAsync(BaseFact.TransactionRecordId);

            Assert.NotNull(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task WatchlistGetTransactionStatusTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Watchlist.GetTransactionStatusAsync(BaseFact.TransactionId);

            Assert.NotNull(response);
        }

        #region Member Data

        public static IEnumerable<object[]> VerifyTestData()
        {
            yield return new object[]
            {
                new VerifyRequest
                {
                    PackageId = BaseFact.PackageId,
                    VerificationType = VerificationType.Test,
                    VerboseMode = true,
                    CountryCode = "CA",
                    DataFields = new DataFields
                    {
                        PersonInfo = new PersonInfo
                        {
                            FirstGivenName = "FirstGivenName",
                            MiddleName = "MiddleName",
                            FirstSurName = "FirstSurName",
                            SecondSurname = "SecondSurname",
                            ISOLatin1Name = "ISOLatin1Name",
                            DayOfBirth = 1,
                            MonthOfBirth = 1,
                            YearOfBirth = 2000,
                            MinimumAge = 1,
                            Gender = "M"
                        },
                        Location = new Location
                        {
                            BuildingNumber = "123",
                            BuildingName = "BuildingName",
                            UnitNumber = "204",
                            StreetName = "Main",
                            StreetType = "Street",
                            City = "City",
                            Suburb = "Suburb",
                            County = "County",
                            StateProvinceCode = "BC",
                            Country = "CA",
                            PostalCode = "A1A1A1"
                        }
                    }

                }
            };
        }

        public static IEnumerable<object[]> TransactionStatusTestData()
        {
            yield return new object[]
            {
                new TransactionStatus
                {
                    TransactionId = "",
                    IsTimedOut = false
                }
            };
        }

        #endregion
    }
}
