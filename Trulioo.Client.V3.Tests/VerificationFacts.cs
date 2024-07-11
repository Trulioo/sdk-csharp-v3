using Newtonsoft.Json;
using System.Net;
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
    public class VerificationFacts
    {
        [Theory(Skip = "Calls API")]
        [MemberData(nameof(IdvVerificationTestData))]
        public async Task IdvVerificationTest(VerifyRequest request, VerifyResult expectedResult)
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Verification.VerifyAsync(request);

            Assert.NotNull(response);
        }

        [Theory(Skip = "Calls API")]
        [MemberData(nameof(TransactionRecordTestData))]
        public async Task GetTransactionRecordTest(string transactionRecordId, TransactionRecordResult expecTransactionRecordResult)
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Verification.GetTransactionRecordAsync(transactionRecordId);

            Assert.Equal(expecTransactionRecordResult.TransactionID, response.TransactionID);
        }

        [Theory(Skip = "Calls API")]
        [MemberData(nameof(TransactionStatusTestData))]
        public async Task GetTransactionStatusTest(string transactionID, TransactionStatus expectedTransactionStatus)
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Verification.GetTransactionStatusAsync(transactionID);

            Assert.Equal(expectedTransactionStatus.Status, response.Status);
            Assert.Equal(expectedTransactionStatus.TransactionId, response.TransactionId);
            Assert.Equal(expectedTransactionStatus.TransactionRecordId, response.TransactionRecordId);
            Assert.Equal(expectedTransactionStatus.IsTimedOut, response.IsTimedOut);
        }

        [Fact(Skip = "Calls API")]
        public async Task GetPartialResults()
        {
            using var client = await BaseFact.GetTruliooKYBClientAsync();
            var response = await client.Verification.GetPartialResultsAsync(BaseFact.TransactionId);
            Assert.NotNull(response);
        }

        [Theory(Skip = "Calls API")]
        [MemberData(nameof(DownloadDocData))]
        public async Task GetDocumentDownload(string transactionRecordId, string fieldName)
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Verification.GetDocumentDownloadAsync(transactionRecordId, fieldName);
            Assert.NotNull(response);
            
        }

        [Theory(Skip = "Calls API")]
        [MemberData(nameof(TransactionRecordDocumentData))]
        public async Task GetTransactionRecordDocument(string transactionRecordID, string documentField)
        {
            using var client = await BaseFact.GetTruliooClientAsync();
            var response = await client.Verification.GetTransactionRecordDocumentAsync(transactionRecordID, documentField);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task MockedIdvVerificationTest()
        {
            using var client = await BaseFact.GetMockedTruliooClientAsync(new MockedVerificationHandler());
            var response = await client.Verification.VerifyAsync(new VerifyRequest());

            Assert.NotNull(response);
            var appendedFields = response.Record.DatasourceResults.First().AppendedFields;
            Assert.Equal("John", appendedFields.First(f => f.FieldName == "FirstName").Data);
        }

        #region Member Data

        public static IEnumerable<object[]> IdvVerificationTestData()
        {
            yield return new object[]
            {
                new VerifyRequest
                {
                    PackageId = BaseFact.PackageId,
                    VerboseMode = true,
                    CountryCode = "CA",
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

        public static IEnumerable<object[]> DownloadDocData()
        {
            yield return new object[] { "transactionRecordId", "fieldName" };
        }

        public static IEnumerable<object[]> TransactionRecordDocumentData()
        {
            yield return new object[] { "transactionRecordId", "fieldName" };
        }

        #endregion

        #region Mocked MessageHandler

        internal class MockedVerificationHandler : HttpClientHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                if (request.RequestUri.ToString().EndsWith("/connect/token"))
                {
                    var content = new StringContent("{\"access_token\":\"xxxx-token-xxxx\",\"expires_in\":1800,\"token_type\":\"Bearer\",\"scope\":\"napi.api\"}");
                    var response = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = content };
                    return response;
                }


                if (request.RequestUri.ToString().EndsWith("/verifications/verify"))
                {
                    var verifyResult = new VerifyResult()
                    {
                        TransactionID = Guid.NewGuid().ToString(),
                        UploadedDt = DateTime.Now,
                        CompletedDt = DateTime.Now,
                        CountryCode = BaseFact.CountryCode,
                        ProductName = "Identity Verification",
                        Record = new Record
                        {
                            TransactionRecordID = Guid.NewGuid().ToString(),
                            RecordStatus = "match",
                            DatasourceResults = new []
                            {
                                new DatasourceResult
                                {
                                    DatasourceName = "Mock Datasource",
                                    DatasourceFields = new []
                                    {
                                        new DatasourceField
                                        {
                                            FieldName = "FirstName",
                                            Status = "match"
                                        }
                                    },
                                    AppendedFields = new []
                                    {
                                        new AppendedField
                                        {
                                            FieldName = "FirstName",
                                            Data = "John"
                                        }
                                    },
                                    Errors = Array.Empty<ServiceError>(),
                                    FieldGroups = Array.Empty<string>()
                                }
                            },
                            Errors = Array.Empty<ServiceError>(),
                            Rule = new RecordRule
                            {
                                RuleName = "Mock Rule",
                                Note = "Mock Rule"
                            }
                        },
                        Errors = new List<ServiceError>(),

                    };
                    var content = new StringContent(JsonConvert.SerializeObject(verifyResult));
                    var response = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = content };
                    return response;
                }
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
        #endregion
    }
}
