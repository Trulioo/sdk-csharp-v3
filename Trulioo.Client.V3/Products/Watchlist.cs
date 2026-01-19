using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Trulioo.Client.V3.Models.Account;
using Trulioo.Client.V3.Models.Business;
using Trulioo.Client.V3.Models.Configuration;
using Trulioo.Client.V3.Models.Fields;
using Trulioo.Client.V3.Models.Verification;
using Trulioo.Client.V3.URI;

namespace Trulioo.Client.V3.Products
{
    public class Watchlist : ProductBase
    {
        #region Fields/Properties

        private readonly Namespace _watchlistNameSpace = new Namespace("watchlist");

        private readonly string _version = "v3";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Watchlist"/> class.
        /// </summary>
        /// <param name="client">
        /// An object representing the root of Trulioo Watchlist Product Services.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="client"/> is <c>null</c>.
        /// </exception>
        protected internal Watchlist(TruliooApiClient client) : base(client)
        {

        }

        #endregion

        #region Methods

        /// <summary>
        /// Calling this method will perform a verification. If your account includes address cleansing, 
        /// set the CleansedAddress flag to get additional address information in the result.
        /// </summary>
        /// <param name="request"> Request object containing parameters to verify an individual.</param>
        /// <param name="cancellationToken"> Cancellation Token that can cancel asynchronous operation.</param>
        /// <returns>Verification results</returns>
        public async Task<VerifyResult> VerifyAsync(VerifyRequest request, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "verifications", "verify");
            var response = await Context.PostAsync<VerifyResult>(_watchlistNameSpace, resource, request, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method retrieves all packages in the account.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns>Account Packages info</returns>
        public async Task<IEnumerable<PackageResult>> GetAccountPackagesAsync(CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "account", "packages");
            var response = await Context.GetAsync<IEnumerable<PackageResult>>(_watchlistNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method retrieves details about consents required for data sources currently configured in your account configuration.
        /// The response for this method contains a collection of objects.
        /// Each object contains the Name of the data source, Text outlining what the user is consenting to, and optionally a Url where the user can find more information about how their data will be used.
        /// </summary>
        /// <param name="packageID">Package ID</param>
        /// <param name="countryCode">Call CountryCodes to get the countries available to you.</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns></returns>
        public async Task<IEnumerable<Consent>> GetDetailedConsentsAsync(string packageID, string countryCode, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "configuration", "consents", packageID, countryCode, "detail");
            var response = await Context.GetAsync<IEnumerable<Consent>>(_watchlistNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method retrieves the consents required for data sources currently configured in your account configuration.
        /// The response for this method contains a collection of strings that Verify method's ConsentForDataSources field expects to perform a verification using those data sources.
        /// A failure to provide an element from the string collection will lead to a 1005 service error.
        /// </summary>
        /// <param name="packageID">Package ID</param>
        /// <param name="countryCode">Call CountryCodes to get the countries available to you.</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetConsentsAsync(string packageID, string countryCode, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "configuration", "consents", packageID, countryCode);
            var response = await Context.GetAsync<IEnumerable<string>>(_watchlistNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method retrieves all the country codes for given packageID
        /// </summary>
        /// <param name="packageID">Package ID</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetCountryCodesAsync(string packageID, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "configuration", "countrycodes", packageID);
            var response = await Context.GetAsync<IEnumerable<string>>(_watchlistNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Gets the provinces states or other subdivisions for a country, mostly matches ISO 3166-2
        /// </summary>
        /// <param name="countryCode">Call CountryCodes to get the countries available to you.</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns></returns>
        public async Task<IEnumerable<CountrySubdivision>> GetCountrySubdivisionsAsync(string countryCode, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "configuration", "countrysubdivisions", countryCode);
            var response = await Context.GetAsync<IList<CountrySubdivision>>(_watchlistNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method retrieves all the fields for given packageID and countryCode
        /// </summary>
        /// <param name="packageID">packageID</param>
        /// <param name="countryCode">CountryCodes</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns></returns>
        public async Task<Dictionary<string, dynamic>> GetFieldsAsync(string packageID, string countryCode, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "configuration", "fields", packageID, countryCode);
            var response = await Context.GetAsync<Dictionary<string, dynamic>>(_watchlistNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Gets Recommended Fields based on Country and Package ID.
        /// </summary>
        /// <param name="packageID">Package ID</param>
        /// <param name="countryCode">CountryCode</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns></returns>
        public async Task<Dictionary<string, dynamic>> GetRecommendedFieldsAsync(string packageID, string countryCode, CancellationToken cancellationToken = default)

        {
            var resource = new ResourceName(_version, "configuration", "fields", packageID, countryCode, "recommended");
            var response = await Context.GetAsync<Dictionary<string, dynamic>>(_watchlistNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Gets the test entities configured for your product and country.
        /// </summary>
        /// <param name="packageID">Package ID</param>
        /// <param name="countryCode">Call CountryCodes to get the countries available to you.</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns></returns>
        public async Task<IEnumerable<TestEntityDataFields>> GetTestEntitiesAsync(string packageID, string countryCode, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "configuration", "testentities", packageID, countryCode);
            var response = await Context.GetAsync<IList<TestEntityDataFields>>(_watchlistNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Gets datasource groups configured for your product and country.
        /// </summary>
        /// <param name="packageID">Package ID</param>
        /// <param name="countryCode">Call CountryCodes to get the countries available to you.</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns></returns>
        public async Task<IEnumerable<NormalizedDatasourceGroupCountry>> GetDatasourcesAsync(string packageID, string countryCode, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "configuration", "datasources", packageID, countryCode);
            var response = await Context.GetAsync<IList<NormalizedDatasourceGroupCountry>>(_watchlistNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method enables you to check if your system can connect to the system.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <param name="name">name to be returned</param>
        /// <returns>The name that was in the request</returns>
        public async Task<string> SayHelloAsync(string name, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "connection", "sayhello", name);
            var response = await Context.GetAsync<string>(_watchlistNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
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
            var response = await Context.GetAsync<string>(_watchlistNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
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
            var response = await Context.PostAsync<string>(_watchlistNameSpace, resource, transactionStatus, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method provides the final results once the transaction completes. 
        /// The response will have match signals per Name and Address fields and notify if the individual is verified.
        /// </summary>
        /// <param name="transactionRecordID">The ID of the transaction record, this will be a GUID.</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation.</param>
        /// <returns>The transaction record results.</returns>
        public async Task<TransactionRecordResult> GetTransactionRecordAsync(string transactionRecordID, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "verifications", "transactionrecord", transactionRecordID);
            var response = await Context.GetAsync<TransactionRecordResult>(_watchlistNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method is used to retrieve the processing status of an asynchronous transaction. 
        /// The response includes the processing status, TransactionID, TransactionRecordID, and timeout status.
        /// </summary>
        /// <param name="transactionID">ID of the asynchronous transaction, this will be a GUID.</param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation.</param>
        /// <returns>The current status of the transaction.</returns>
        public async Task<TransactionStatus> GetTransactionStatusAsync(string transactionID, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "verifications", "transaction", transactionID, "status");
            var response = await Context.GetAsync<TransactionStatus>(_watchlistNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        #endregion
    }
}
