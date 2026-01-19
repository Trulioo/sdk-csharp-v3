using Trulioo.Client.V3.Enums;
using Trulioo.Client.V3.Models.Errors;
using Trulioo.Client.V3.Models.Fields;
using Trulioo.Client.V3.Models.Response;
using Trulioo.Client.V3.Models.Verification;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Record = Trulioo.Client.V3.Models.Response.Record;

namespace Trulioo.Client.V3.Tests
{
    public class KycFacts
    {
        [Fact(Skip = "Calls API")]
        public async Task PersonFraudGetAccountPackagesTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyc.GetAccountPackagesAsync();

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task PersonFraudSayHelloTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyc.SayHelloAsync("");

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task PersonFraudTestAuthenticationTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyc.TestAuthenticationAsync();

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Theory(Skip = "Calls API")]
        [MemberData(nameof(AsyncCallbackTransactionStatusTestData))]
        public async Task AsyncCallbackTest(TransactionStatus transactionStatus)
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyc.AsyncCallbackAsync(transactionStatus);

            Assert.NotNull(response);
        }

        [Theory(Skip = "Calls API")]
        [MemberData(nameof(VerificationTestData))]
        public async Task VerificationTest(VerifyRequest request, VerifyResult expectedResult)
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyc.VerifyAsync(request);

            Assert.NotNull(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task GetConsentsTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyc.GetConsentsAsync(BaseFact.PackageId, BaseFact.CountryCode);

            Assert.NotNull(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task GetDetailedConsentsTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyc.GetDetailedConsentsAsync(BaseFact.PackageId, BaseFact.CountryCode);

            Assert.NotNull(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task GetCountryCodesTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyc.GetCountryCodesAsync(BaseFact.PackageId);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task GetCountrySubdivisionsTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyc.GetCountrySubdivisionsAsync(BaseFact.CountryCode);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task GetFieldsTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyc.GetFieldsAsync(BaseFact.PackageId, BaseFact.CountryCode);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task GetRecommendedFieldsTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyc.GetRecommendedFieldsAsync(BaseFact.PackageId, BaseFact.CountryCode);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task GetTestEntitiesTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyc.GetTestEntitiesAsync(BaseFact.PackageId, BaseFact.CountryCode);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact(Skip = "Calls API")]
        public async Task GetDatasourcesTest()
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyc.GetDatasourcesAsync(BaseFact.PackageId, BaseFact.CountryCode);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Theory(Skip = "Calls API")]
        [MemberData(nameof(TransactionRecordTestData))]
        public async Task GetTransactionRecordTest(string transactionRecordId, TransactionRecordResult expectedTransactionRecordResult)
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyc.GetTransactionRecordAsync(transactionRecordId);

            Assert.NotNull(response);
            Assert.Equal(expectedTransactionRecordResult.TransactionID, response.TransactionID);
        }

        [Theory(Skip = "Calls API")]
        [MemberData(nameof(TransactionRecordDocumentData))]
        public async Task GetTransactionRecordDocumentTest(string transactionRecordID, string documentField)
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyc.GetTransactionRecordDocumentAsync(transactionRecordID, documentField);

            Assert.NotNull(response);
        }

        [Theory(Skip = "Calls API")]
        [MemberData(nameof(TransactionStatusTestData))]
        public async Task GetTransactionStatusTest(string transactionID, TransactionStatus expectedTransactionStatus)
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Kyc.GetTransactionStatusAsync(transactionID);

            Assert.Equal(expectedTransactionStatus.Status, response.Status);
            Assert.Equal(expectedTransactionStatus.TransactionId, response.TransactionId);
            Assert.Equal(expectedTransactionStatus.TransactionRecordId, response.TransactionRecordId);
            Assert.Equal(expectedTransactionStatus.IsTimedOut, response.IsTimedOut);
        }

        [Theory(Skip = "Calls API")]
        [InlineData(true)]
        [InlineData(false)]

        public async Task GetReportStreamTest(bool additionalData)
        {
            using var client = await BaseFact.GetTruliooClientAsync();

             Stream reportStream = null;
             try
             {
                 reportStream = await client.Kyc.GetReportStreamAsync(BaseFact.TransactionRecordId, additionalData);
                 Assert.NotNull(reportStream);
             }
             finally
             {
                 reportStream?.Dispose();
             }
        }

        [Theory(Skip = "Calls API")]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetReportDocumentDownloadTest(bool additionalData)
        {
            using var client = await BaseFact.GetTruliooClientAsync();

            var response = await client.Kyc.GetReportDownloadDocumentAsync(BaseFact.TransactionRecordId, additionalData);
            Assert.NotNull(response);
        }

        #region Member Data

        public static IEnumerable<object[]> AsyncCallbackTransactionStatusTestData()
        {
            yield return new object[]
            {
                new TransactionStatus
                {
                    TransactionId = BaseFact.TransactionId,
                    IsTimedOut = false
                }
            };
        }

        public static IEnumerable<object[]> VerificationTestData()
        {
            yield return new object[]
            {
                new VerifyRequest
                {
                    PackageId = BaseFact.PackageId,
                    VerboseMode = true,
                    CountryCode = "US",
                    CustomerReferenceID = "CustomerReferenceID-1",
                    VerificationType = VerificationType.Test,
                    DataFields = new DataFields
                    {
                        PersonInfo = new PersonInfo
                        {
                            FirstGivenName = "test",
                            FirstSurName = "test",
                            YearOfBirth = 1980
                        },
                        Communication = new Communication
                        {
                            EmailAddress = "email@trulioo.com"
                        },
                        Risk = new RiskMonitorSettings
                        {
                            Action = "Action",
                            CallbackUrl = "CallbackUrl",
                            DeviceID = "DeviceID",
                            Email = "Email",
                            Frequency = "Frequency",
                            IP = "IP",
                            Phone = "Phone",
                            UserAgent = "UserAgent",
                        },
                        Location = new Location()
                        {
                            POBox = "POBox",
                        }
                    }
                },
                new VerifyResult
                {

                },
            };
        }

        public static IEnumerable<object[]> TransactionRecordTestData()
        {
            TransactionRecordResult transactionRecordResult;
            if (string.IsNullOrWhiteSpace(BaseFact.TransactionResult))
            {
                transactionRecordResult = new TransactionRecordResult()
                {
                    InputFields = new List<DataField>() { new DataField { FieldName = "", Value = "" } },
                    TransactionID = "",
                    Record = new Record()
                    {
                        RecordStatus = "",
                        DatasourceResults = new List<DatasourceResult>()
                        {
                            new DatasourceResult()
                            {
                                DatasourceName = "",
                                DatasourceFields = new List<DatasourceField>()
                                {
                                    new DatasourceField() { FieldName = "", Status = "" },
                                    new DatasourceField() { FieldName = "", Status = "" }
                                }
                            }
                        }
                    },
                    Errors = new List<ServiceError> { }
                };
            }
            else
            {
                transactionRecordResult = JsonSerializer.Deserialize<TransactionRecordResult>(BaseFact.TransactionResult);
            }

            yield return new object[] { BaseFact.TransactionRecordId, transactionRecordResult };
        }

        public static IEnumerable<object[]> TransactionRecordDocumentData()
        {
            yield return new object[] { BaseFact.TransactionRecordId, "DocumentFrontImage" };
        }

        public static IEnumerable<object[]> TransactionStatusTestData()
        {
            TransactionStatus transactionStatus;

            if (string.IsNullOrWhiteSpace(BaseFact.TransactionStatus))
            {
                transactionStatus = new TransactionStatus()
                {
                    TransactionId = "",
                    TransactionRecordId = "",
                    Status = "",
                    IsTimedOut = false
                };
            }
            else
            {
                transactionStatus = JsonSerializer.Deserialize<TransactionStatus>(BaseFact.TransactionStatus);
            }

            yield return new object[]
            {
                BaseFact.TransactionId, transactionStatus
            };
        }

        #endregion

    }
}
