using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trulioo.Client.V3.Models.Business;
using Trulioo.Client.V3.Models.Fields;
using Trulioo.Client.V3.Models.Verification;
using Trulioo.Client.V3.URI;

namespace Trulioo.Client.V3.Products
{
    public class Kyb : ProductBase
    {
        #region Private Properties

        private readonly Namespace _kybNameSpace = new Namespace("kyb");

        private readonly string _version = "v3";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Kyb"/> class.
        /// </summary>
        /// <param name="client">
        /// An object representing the root of Trulioo KYB Product Services.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="client"/> is <c>null</c>.
        /// </exception>
        protected internal Kyb(TruliooApiClient client) : base(client)
        {
           
        }

        #endregion

        #region Methods

        /// <summary>
        /// Business search call for Trulioo API KYB Client
        /// </summary>
        /// <param name="request"> Request object containing parameters to search for </param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns> List of possible businesses from search </returns>
        public async Task<BusinessSearchResponse> BusinessSearchAsync(BusinessSearchRequest request, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "business", "search");
            var response = await Context.PostAsync<BusinessSearchResponse>(_kybNameSpace, resource, request, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Business verification call for Trulioo API KYB Client
        /// </summary>
        /// <param name="request"> Request object containing parameters to verify business </param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns> Verification results </returns>
        public async Task<VerifyResult> BusinessVerifyAsync(BusinessVerifyRequest request, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "business", "verify");
            var response = await Context.PostAsync<VerifyResult>(_kybNameSpace, resource, request, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method retrieves the transaction record from the transaction record id
        /// </summary>
        /// <param name="transactionRecordId">Transaction Record Id</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        public async Task<BusinessSearchResponse> GetTransactionRecordAsync(string transactionRecordId, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "business", "search", "transactionrecord", transactionRecordId);
            var response = await Context.GetAsync<BusinessSearchResponse>(_kybNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method retrieves all the fields codes for given packageID
        /// </summary>
        /// <param name="packageID">PackageID</param>
        /// <param name="countryCode">Call CountryCodes to get the countries available to you.</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        public async Task<Dictionary<string, dynamic>> GetFieldsAsync(string packageID, string countryCode, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "configuration", "fields", packageID, countryCode);
            var response = await Context.GetAsync<Dictionary<string, dynamic>>(_kybNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Gets the requested type of business report for the supplied transaction record.
        /// </summary>
        /// <param name="transactionRecordId">Transaction record id that contains the document</param>
        /// <param name="additionalData"></param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        ///<returns>A <see cref="DownloadDocument"/> object representing the generated business report.</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<DownloadDocument> GetBusinessReportAsync(string transactionRecordId, bool additionalData = false, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "business", "report", transactionRecordId);

            var queryParams = new Dictionary<string, string>();
            if (additionalData)
            {
                queryParams.Add("additionalData", "true");
            }

            var response = await Context.GetAsyncWithQueryParams(_kybNameSpace, resource, queryParams, processResponse: parseDownloadDocumentResponse, cancellationToken).ConfigureAwait(false);
            return await response;
        }

        /// <summary>
        /// Gets the requested type of business report for the supplied transaction record.
        /// </summary>
        /// <param name="transactionRecordId">Transaction record id that contains the document</param>
        /// <param name="additionalData"></param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns>The generated business report</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<Stream> BusinessReportAsync(string transactionRecordId, bool additionalData = false, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "business", "report", transactionRecordId);

            var queryParams = new Dictionary<string, string>();
            if (additionalData)
            {
                queryParams.Add("additionalData", "true");
            }

            var response = await Context.GetStreamAsync(_kybNameSpace, resource, queryParams, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method retrieves all the country codes for given packageID
        /// </summary>
        /// <param name="packageID">Package ID</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        public async Task<IEnumerable<string>> GetCountryCodesAsync(string packageID, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "configuration", "countrycodes", packageID);
            var response = await Context.GetAsync<IEnumerable<string>>(_kybNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Gets all jurisdictions of incorporation for all countries if no country is supplied. Gets the jurisdictions of incorporation for a country, if country is supplied.
        /// </summary>
        /// <param name="countryCode">Country alpha2 code </param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        public async Task<IList<CountrySubdivision>> GetCountryJOIAsync(string countryCode = null, CancellationToken cancellationToken = default)
        {
            ResourceName resource;
            if (string.IsNullOrEmpty(countryCode))
            {
                resource = new ResourceName(_version, "business", "countryJOI");
            }
            else
            {
                resource = new ResourceName(_version, "business", "countryJOI", countryCode);
            }

            var response = await Context.GetAsync<IList<CountrySubdivision>>(_kybNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;

        }

        /// <summary>
        /// Gets the test entities configured for your product and country.
        /// </summary>
        /// <param name="packageID">PackageID</param>
        /// <param name="countryCode">Call CountryCodes to get the countries available to you.</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        public async Task<IEnumerable<TestEntityDataFields>> GetTestEntitiesAsync(string packageID, string countryCode, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "configuration", "testentities", packageID, countryCode);
            var response = await Context.GetAsync<IList<TestEntityDataFields>>(_kybNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Gets the currently configured business registration numbers, for optionally supplied country and jurisdiction
        /// A country must be supplied in order to use a jurisdiction.
        /// </summary>
        /// <param name="countryCode"> Optional country alpha2 code </param>
        /// <param name="jurisdictionCode"> Optional jurisdiction code </param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns>  </returns>
        public async Task<IEnumerable<BusinessRegistrationNumber>> GetBusinessRegistrationNumbersAsync(string countryCode = null, string jurisdictionCode = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(countryCode) && !string.IsNullOrWhiteSpace(jurisdictionCode))
            {
                throw new ArgumentException("Cannot use jurisdiction without a country.");
            }
            var resourceParams = new List<string> { _version, "business", "businessregistrationnumbers", countryCode, jurisdictionCode }.Where(x => !string.IsNullOrWhiteSpace(x));
            var resource = new ResourceName(resourceParams);
            var response = await Context.GetAsync<IEnumerable<BusinessRegistrationNumber>>(_kybNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method is used to retrieve the partial result of an asynchronous transaction.
        /// </summary>
        /// <param name="transactionId">id of the asynchronous transaction, this will be a GUID</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns>Partial Verify Result</returns>
        public async Task<VerifyResultPartial> GetPartialResultsAsync(string transactionId, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "verifications", "transaction", transactionId, "partialresult");
            var response = await Context.GetAsync<VerifyResultPartial>(_kybNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Downloads a specific document associated with a transaction record.
        /// </summary>
        /// <param name="transactionRecordId">Transaction Record ID</param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns>A <see cref="Stream"/>Document</returns>
        /// <exception cref="Trulioo.Client.V3.Exceptions.RequestException">Thrown if the API returns an error (e.g., unauthorized, document not found).</exception>
        public async Task<Stream> DocumentDownloadAsync(string transactionRecordId, string fieldName, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "verifications", "documentdownload", transactionRecordId, fieldName);
            var response = await Context.GetStreamAsync(_kybNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Downloads a specific document associated with a transaction record.
        /// </summary>
        /// <param name="transactionRecordId">Transaction Record ID</param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns>A <see cref="DownloadDocument"/>Document</returns>
        /// <exception cref="Trulioo.Client.V3.Exceptions.RequestException">Thrown if the API returns an error (e.g., unauthorized, document not found).</exception>
        public async Task<DownloadDocument> GetDocumentDownloadAsync(string transactionRecordId, string fieldName, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "verifications", "documentdownload", transactionRecordId, fieldName);
            var response = await Context.GetAsync(_kybNameSpace, resource, processResponse: parseDownloadDocumentResponse, cancellationToken: cancellationToken).ConfigureAwait(false);
            return await response;
        }


        #endregion

        #region Privates/internals

        private async Task<DownloadDocument> parseDownloadDocumentResponse(HttpResponseMessage response)
        {
            var rawMessage = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

            var filename = "downloadDocument.pdf";
            if (!string.IsNullOrEmpty(response.Content.Headers.ContentDisposition?.FileName))
            {
                filename = response.Content.Headers.ContentDisposition.FileName;
            }

            return new DownloadDocument
            {
                DocumentName = filename,
                DocumentContent = rawMessage
            };
        }

        #endregion
    }
}
