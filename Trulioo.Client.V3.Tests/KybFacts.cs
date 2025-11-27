using Trulioo.Client.V3.Enums;
using Trulioo.Client.V3.Models.Business;
using Trulioo.Client.V3.Tests.MemberData;
using Xunit;

namespace Trulioo.Client.V3.Tests
{
    public class KybFacts
    {
        [Theory(Skip = "Calls API")]
        [MemberData(nameof(KybTestData.BusinessSearchTestData), MemberType = typeof(KybTestData))]
        public async Task KybBusinessSearchTest(BusinessSearchRequest request, BusinessSearchResponse expectedResponse)
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyb.BusinessSearchAsync(request);

            Assert.NotNull(response);
        }

        [Theory(Skip = "Calls API")]
        [MemberData(nameof(KybTestData.BusinessVerifyTestData), MemberType = typeof(KybTestData))]
        public async void KybBusinessVerifyTest(BusinessVerifyRequest request)
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyb.BusinessVerifyAsync(request);

            Assert.NotNull(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task KybGetBusinessTransactionRecord()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyb.GetTransactionRecordAsync(BaseFact.TransactionId);
            Assert.NotNull(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task KybGetFieldsTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyb.GetFieldsAsync(BaseFact.PackageId, BaseFact.CountryCode);
            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task KybGetCountryCodesTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyb.GetCountryCodesAsync(BaseFact.PackageId);
            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Theory(Skip = "Calls API")]
        [InlineData("20cd8afb-ff14-7255-a333-e3d02a1086dc", false)]
        [InlineData("20cd8afb-ff14-7255-a333-e3d02a1086dc", true)]
        public async Task KybBusinessReportTest(string transactionRecordId, bool additionalData)
        {

            using var client = await BaseFact.GetTruliooClientAsync();

            Stream reportStream = null;
            try
            {
                reportStream = await client.Kyb.BusinessReportAsync(transactionRecordId, additionalData);
                Assert.NotNull(reportStream);
            }
            finally
            {
                reportStream?.Dispose();
            }
        }

        [Theory(Skip = "Calls API")]
        [InlineData("20cd8afb-ff14-7255-a333-e3d02a1086dc", false)]
        [InlineData("20cd8afb-ff14-7255-a333-e3d02a1086dc", true)]
        public async Task KybGetBusinessReportTest(string transactionRecordId, bool additionalData)
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyb.GetBusinessReportAsync(transactionRecordId, additionalData);
            Assert.NotNull(response);
        }

        [Theory(Skip = "Calls API")]
        [InlineData(null)] // Test case for getting all jurisdictions
        [InlineData("US")] // Test case for a specific country (United States)
        [InlineData("CA")] // Test case for another specific country (Canada)
        public async Task KybGetCountryJOITest(string countryCode)
        {
            using var client = await BaseFact.GetTruliooClientAsync();

            IList<CountrySubdivision> jurisdictions;

            if (countryCode == null)
            {
                jurisdictions = await client.Kyb.GetCountryJOIAsync();
                Assert.NotNull(jurisdictions);
                Assert.NotEmpty(jurisdictions);
                Assert.True(jurisdictions.Count > 10, "Expected a significant number of jurisdictions when fetching all."); // Arbitrary check for "many"
            }
            else
            {
                jurisdictions = await client.Kyb.GetCountryJOIAsync(countryCode);
                Assert.NotNull(jurisdictions);
                Assert.NotEmpty(jurisdictions);
            }
        }

        [Fact(Skip = "Calls API")]
        public async Task KybGetTestEntitiesTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyb.GetTestEntitiesAsync(BaseFact.PackageId, BaseFact.CountryCode);
            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Theory(Skip = "Calls API")]
        [InlineData("2099eec5-ba25-f5cf-e8bd-e0897b47f549", "ShareholderListDocument")]
        public async Task KybDocumentDownloadTest(string transactionRecordId, string fieldName)
        {
            using var client = await BaseFact.GetTruliooClientAsync();

            Stream downloadedStream = null;
            try
            {
                downloadedStream = await client.Kyb.DocumentDownloadAsync(transactionRecordId, fieldName);

                Assert.NotNull(downloadedStream);
                Assert.True(downloadedStream.CanRead, "Downloaded stream should be readable.");
                Assert.True(downloadedStream.Length > 0, "Downloaded document stream should not be empty.");
            }
            finally
            {
                downloadedStream?.Dispose();
            }
        }

        [Theory(Skip = "Calls API")]
        [InlineData("2099eec5-ba25-f5cf-e8bd-e0897b47f549", "ShareholderListDocument")]
        public async Task KybGetDocumentDownloadTest(string transactionRecordId, string fieldName)
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyb.GetDocumentDownloadAsync(transactionRecordId, fieldName);
            Assert.NotNull(response);
        }

        [Theory(Skip = "Calls API")]
        [InlineData("CA", null)]
        [InlineData("CA", "BC")]
        [InlineData(null, null)]
        [InlineData(" ", "")]
        [InlineData("", "BC")]
        [InlineData(null, "BC")]
        public async Task KybGetBusinessRegistrationNumbers(string countryCode, string jurisdiction)
        {
            using var client = await BaseFact.GetTruliooClientAsync();

            if (string.IsNullOrWhiteSpace(countryCode) && !string.IsNullOrWhiteSpace(jurisdiction))
            {
                await Assert.ThrowsAsync<ArgumentException>(async () => await client.Kyb.GetBusinessRegistrationNumbersAsync(countryCode, jurisdiction));
            }
            else
            {
                var response = await client.Kyb.GetBusinessRegistrationNumbersAsync(countryCode, jurisdiction);
                Assert.NotNull(response);
            }
        }

        [Fact(Skip = "Calls API")]
        public async Task KybGetPartialResults()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyb.GetPartialResultsAsync(BaseFact.TransactionId);
            Assert.NotNull(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task KybCancellationTokenTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();

            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(10));
            await Task.Delay(10);

            await Assert.ThrowsAsync<TaskCanceledException>(async () => await client.Kyb.GetTestEntitiesAsync(BaseFact.PackageId, BaseFact.CountryCode, cancellationTokenSource.Token));
        }

        [Fact]
        public async Task BusinessMonitoringVerification()
        {
            using var client = await BaseFact.GetTruliooKYBClientAsync();
            var request = new BusinessVerifyRequest
            {
                PackageId = BaseFact.PackageId,
                VerificationType = VerificationType.Live,
                VerboseMode = true,
                CountryCode = BaseFact.CountryCode,
                BusinessDataFields = new BusinessDataFields
                {
                    BusinessName = "PREMIUM PSYCHIATRIC CARE NURSING - PC",
                    BusinessRegistrationNumber = "6123001",
                    JurisdictionOfIncorporation = "CA",
                    EnhancedProfile = true,
                    KYBMonitoringFields = "BusinessStatus,BusinessLegalForm,StandardizedLocations,StandardizedIndustries,StandardizedCompanyOwnershipHierarchy,StandardizedCommunication,StandardizedIncorporationDetails,StandardizedBusinessNames",
                    KYBMonitoringCallbackURL = "",
                    KYBMonitoringEnabled = false,
                    KYBMonitoringFrequency = "Monthly",
                    KYBMonitoringHistory = false
                }
            };
            var response = await client.Kyb.BusinessVerifyAsync(request);
            Assert.NotNull(response);
        }

    }
}