using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Trulioo.Client.V3.Helpers;
using Trulioo.Client.V3.Models.Account;
using Trulioo.Client.V3.Models.Business;
using Trulioo.Client.V3.Models.Configuration;
using Trulioo.Client.V3.Models.Fields;
using Trulioo.Client.V3.Models.Verification;
using Trulioo.Client.V3.URI;

namespace Trulioo.Client.V3.Products
{
    public class Kyc : ProductBase
    {
        #region Private Properties

        private readonly Namespace _kycNameSpace = new Namespace("kyc");

        private readonly string _version = "v3";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Kyc"/> class.
        /// </summary>
        /// <param name="client">
        /// An object representing the root of Trulioo KYC Product Services.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="client"/> is <c>null</c>.
        /// </exception>
        protected internal Kyc(TruliooApiClient client) : base(client)
        {

        }

        #endregion

        #region Methods

        /// <summary>
        /// This method retrieves all packages in the account.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns>Account Packages info</returns>
        public async Task<IEnumerable<PackageResult>> GetAccountPackagesAsync(CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "account", "packages");
            var response = await Context.GetAsync<IEnumerable<PackageResult>>(_kycNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method enables you to check if your system can connect to the system.
        /// </summary>
        /// <param name="name">name to be returned</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns>The name that was in the request</returns>
        public async Task<string> SayHelloAsync(string name, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "connection", "sayhello", name);
            var response = await Context.GetAsync<string>(_kycNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method enables you to check if your system can connect to the system.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns>Your client id</returns>
        public async Task<string> TestAuthenticationAsync(CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "connection", "testauthentication");
            var response = await Context.GetAsync<string>(_kycNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// If set, the transaction will run asynchronously and Trulioo will try to update the client with transaction state updates until completed. If callback is not desired but the client wants to initiate an asynchronous transaction, provide https://api.globaldatacompany.com/v3/connection/async-callback as the Callback URL.
        /// </summary>
        /// <param name="transactionStatus">transactionStatus</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns></returns>
        public async Task<string> AsyncCallbackAsync(TransactionStatus transactionStatus, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "connection", "async-callback");
            var response = await Context.PostAsync<string>(_kycNameSpace, resource, transactionStatus, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// The verification call for the Trulioo API V3
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns> Verification results </returns>
        public async Task<VerifyResult> VerifyAsync(VerifyRequest request, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "verifications", "verify");
            var response = await Context.PostAsync<VerifyResult>(_kycNameSpace, resource, request, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method retrieves the consents required for data sources currently configured in your account configuration. 
        /// The response for this method contains a collection of strings that Verify method's ConsentForDataSources field expects to perform a verification using those data sources. 
        /// A failure to provide an element from the string collection will lead to a <a class='link-to-api' href='#errors'>1005</a> service error.
        /// </summary>
        /// <param name="packageID">Package ID</param>
        /// <param name="countryCode">Call CountryCodes to get the countries available to you.</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetConsentsAsync(string packageID, string countryCode, CancellationToken cancellationToken = default)
        {
            var resourceParams = new List<string> { _version, "configuration", "consents", packageID, countryCode };
            var resource = new ResourceName(resourceParams);
            var response = await Context.GetAsync<IEnumerable<string>>(_kycNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        ///<summary>
        /// This method retrieves details about consents required for data sources currently configured in your account configuration. 
        /// The response for this method contains a collection of objects.
        /// Each object contains the Name of the data source, Text outlining what the user is consenting to, and optionally a Url where the user can find more information about how their data will be used.  
        /// Failure to provide a Name from the object collection will lead to a <a class='link-to-api' href='#errors'>1005</a> service error.
        ///</summary>
        /// <param name="packageID">Call packageID to get the countries available to you.</param>
        /// <param name="countryCode">Call CountryCodes to get the countries available to you.</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns></returns>
        public async Task<IEnumerable<Consent>> GetDetailedConsentsAsync(string packageID, string countryCode, CancellationToken cancellationToken = default)
        {
            var resourceParams = new List<string> { _version, "configuration", "consents", packageID, countryCode, "detail" };
            var resource = new ResourceName(resourceParams);
            var response = await Context.GetAsync<IEnumerable<Consent>>(_kycNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method retrieves all the country codes for given packageID
        /// </summary>
        /// <param name="packageID">Package ID</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns>country codes</returns>
        public async Task<IEnumerable<string>> GetCountryCodesAsync(string packageID, CancellationToken cancellationToken = default)
        {
            var resourceParams = new List<string> { _version, "configuration", "countrycodes", packageID };
            var resource = new ResourceName(resourceParams);
            var response = await Context.GetAsync<IEnumerable<string>>(_kycNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Gets the provinces states or other subdivisions for a country, mostly matches ISO 3166-2
        /// </summary>
        /// <param name="countryCode">Country alpha2 code, get the call to countrycodes</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns>country subdivisions</returns>
        public async Task<IEnumerable<CountrySubdivision>> GetCountrySubdivisionsAsync(string countryCode, CancellationToken cancellationToken = default)
        {
            var resourceParams = new List<string> { _version, "configuration", "countrysubdivisions", countryCode };
            var resource = new ResourceName(resourceParams);
            var response = await Context.GetAsync<IEnumerable<CountrySubdivision>>(_kycNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method retrieves all the fields codes for given packageID
        /// </summary>
        /// <param name="packageID">PackageID</param>
        /// <param name="countryCode">Call CountryCodes to get the countries available to you.</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns>field codes</returns>
        public async Task<Dictionary<string, dynamic>> GetFieldsAsync(string packageID, string countryCode, CancellationToken cancellationToken = default)
        {
            var resourceParams = new List<string> { _version, "configuration", "fields", packageID, countryCode };
            var resource = new ResourceName(resourceParams);
            var response = await Context.GetAsync<Dictionary<string, dynamic>>(_kycNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method recommended retrieves all the fields codes for given packageID
        /// </summary>
        /// <param name="packageID">PackageID</param>
        /// <param name="countryCode">Call CountryCodes to get the countries available to you.</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns>recommended field codes</returns>
        public async Task<Dictionary<string, dynamic>> GetRecommendedFieldsAsync(string packageID, string countryCode, CancellationToken cancellationToken = default)
        {
            var resourceParams = new List<string> { _version, "configuration", "fields", packageID, countryCode, "recommended" };
            var resource = new ResourceName(resourceParams);
            var response = await Context.GetAsync<Dictionary<string, dynamic>>(_kycNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Gets the test entities configured for your product and country.
        /// </summary>
        /// <param name="packageID"></param>
        /// <param name="countryCode">Call CountryCodes to get the countries available to you.</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns>test entities</returns>
        public async Task<IEnumerable<TestEntityDataFields>> GetTestEntitiesAsync(string packageID, string countryCode, CancellationToken cancellationToken = default)
        {
            var resourceParams = new List<string> { _version, "configuration", "testentities", packageID, countryCode };
            var resource = new ResourceName(resourceParams);
            var response = await Context.GetAsync<IEnumerable<TestEntityDataFields>>(_kycNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Gets datasource groups configured for your product and country.
        /// </summary>
        /// <param name="packageID">Package ID</param>
        /// <param name="countryCode">Call CountryCodes to get the countries available to you.</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns>datasource groups</returns>
        public async Task<IEnumerable<NormalizedDatasourceGroupCountry>> GetDatasourcesAsync(string packageID, string countryCode, CancellationToken cancellationToken = default)
        {
            var resourceParams = new List<string> { _version, "configuration", "datasources", packageID, countryCode };
            var resource = new ResourceName(resourceParams);
            var response = await Context.GetAsync<IEnumerable<NormalizedDatasourceGroupCountry>>(_kycNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Gets transaction record information
        /// </summary>
        /// <param name="transactionRecordId">The ID of the transaction record (TRID). This is a GUID</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns>transaction record result</returns>
        public async Task<TransactionRecordResult> GetTransactionRecordAsync(string transactionRecordId, CancellationToken cancellationToken = default)
        {
            var resourceParams = new List<string> { _version, "verifications", "transactionrecord", transactionRecordId };
            var resource = new ResourceName(resourceParams);
            var response = await Context.GetAsync<TransactionRecordResult>(_kycNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method is used to retrieve the document of a verification performed using the verify method.
        /// The response for this method includes the processed base64 JPEG formatted string
        /// </summary>
        /// <param name="transactionRecordID">The ID of the transaction record (TRID). This is a GUID</param>
        /// <param name="documentField">FieldName of the Document, this will be a string</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns>string</returns>
        public async Task<string> GetTransactionRecordDocumentAsync(string transactionRecordId, string documentField,  CancellationToken cancellationToken = default)
        {
            var resourceParams = new List<string> { _version, "verifications", "transactionrecord", transactionRecordId, documentField };
            var resource = new ResourceName(resourceParams);
            var response = await Context.GetAsync<string>(_kycNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method is used to retrieve the processing status of an asynchronous transaction. The response for this method includes the processing status of the verification, the TransactionID, the TransactionRecordID as well as whether the verification request has timed out.
        /// </summary>
        /// <param name="transactionID">id of the asynchronous transaction, this will be a GUID</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns>TransactionStatus</returns>
        public async Task<TransactionStatus> GetTransactionStatusAsync(string transactionId, CancellationToken cancellationToken = default)
        {
            var resourceParams = new List<string> { _version, "verifications", "transaction", transactionId, "status" };
            var resource = new ResourceName(resourceParams);
            var response = await Context.GetAsync<TransactionStatus>(_kycNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Gets the requested type of Kyc report for the supplied transaction record.
        /// </summary>
        /// <param name="transactionRecordID">The ID of the transaction record (TRID). This is a GUID</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns>The generated business report</returns>
        public async Task<DownloadDocument> GetReportDownloadDocumentAsync(string transactionRecordId, bool additionalData = false, CancellationToken cancellationToken = default)
        {
            var resourceParams = new List<string> { _version, "report", transactionRecordId };
            var resource = new ResourceName(resourceParams);

            var queryParams = new Dictionary<string, string>();
            if (additionalData)
            {
                queryParams.Add("additionalData", "true");
            }

            var response = await Context.GetAsyncWithQueryParams(_kycNameSpace, resource, queryParams, processResponse: DownloadDocumentHelper.ParseDownloadDocumentResponseAsync, cancellationToken: cancellationToken).ConfigureAwait(false);
            return await response;
        }

        /// <summary>
        /// Gets the requested type of Kyc report for the supplied transaction record.
        /// </summary>
        /// <param name="transactionRecordID">The ID of the transaction record (TRID). This is a GUID</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns>The generated business report</returns>
        public async Task<Stream> GetReportStreamAsync(string transactionRecordId, bool additionalData = false, CancellationToken cancellationToken = default)
        {
            var resourceParams = new List<string> { _version, "report", transactionRecordId };
            var resource = new ResourceName(resourceParams);

            var queryParams = new Dictionary<string, string>();
            if (additionalData)
            {
                queryParams.Add("additionalData", "true");
            }

            var response = await Context.GetStreamAsync(_kycNameSpace, resource, queryParams, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        #endregion
    }
}
