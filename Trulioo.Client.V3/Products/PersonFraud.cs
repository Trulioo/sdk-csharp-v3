using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Trulioo.Client.V3.Models.Business;
using Trulioo.Client.V3.Models.Configuration;
using Trulioo.Client.V3.Models.Fields;
using Trulioo.Client.V3.Models.Risk;
using Trulioo.Client.V3.URI;

namespace Trulioo.Client.V3.Products
{

    /// <summary>
    /// Provides a class for working with Trulioo Person Fraud.
    /// </summary>
    public class PersonFraud : ProductBase
    {
        #region Fields/Properties

        private readonly Namespace _personFraudNameSpace = new Namespace("personfraud");

        private readonly string _version = "v3";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonFraud"/> class.
        /// </summary>
        /// <param name="client">
        /// An object representing the root of Trulioo Person Fraud Services.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="client"/> is <c>null</c>.
        /// </exception>
        protected internal PersonFraud(TruliooApiClient client) : base(client)
        {

        }

        #endregion

        #region Methods


        /// <summary>
        /// Person Fraud Check for Trulioo API Client V3
        /// </summary>
        /// <param name="request"> Request object containing parameters for a Person Fraud Check </param>
        /// <param name="cancellationToken">Cancellation Token that can cancel asynchronous operation</param>
        /// <returns> Risk Verify Result </returns>
        public async Task<RiskVerifyResult> PersonFraudCheckAsync(RiskVerifyRequest request, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceName(_version, "check");
            var response = await Context.PostAsync<RiskVerifyResult>(_personFraudNameSpace, resource, request, cancellationToken).ConfigureAwait(false);
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
            var response = await Context.GetAsync<IList<NormalizedDatasourceGroupCountry>>(_personFraudNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
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
            var response = await Context.GetAsync<IList<TestEntityDataFields>>(_personFraudNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
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
            var response = await Context.GetAsync<IList<CountrySubdivision>>(_personFraudNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
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
            var response = await Context.GetAsync<IEnumerable<string>>(_personFraudNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
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
            var response = await Context.GetAsync<Dictionary<string, dynamic>>(_personFraudNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
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
            var response = await Context.GetAsync<Dictionary<string, dynamic>>(_personFraudNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
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
            var response = await Context.GetAsync<IEnumerable<Consent>>(_personFraudNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
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
            var response = await Context.GetAsync<IEnumerable<string>>(_personFraudNameSpace, resource, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        #endregion
    }
}