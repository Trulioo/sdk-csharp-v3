using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trulioo.Client.V3.Models.Business;
using Trulioo.Client.V3.Models.Configuration;
using Trulioo.Client.V3.Models.Fields;
using Trulioo.Client.V3.URI;

namespace Trulioo.Client.V3
{
    /// <summary>
    /// Provides a class for working with Trulioo Configuration.
    /// </summary>
    public class Configuration
    {
        #region Private Properties

        private TruliooApiClient _service;
        private readonly Namespace _configurationNamespace = new Namespace("configuration");

        private Context _context
        {
            get { return _service.Context; }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        /// <param name="service">
        /// An object representing the root of Trulioo configuration service.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="service"/> is <c>null</c>.
        /// </exception>
        protected internal Configuration(TruliooApiClient service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        #endregion

        #region Methods

        /// <summary>
        /// This method retrieves the consents required for data sources currently configured in your account configuration. 
        /// The response for this method contains a collection of strings that Verify method's ConsentForDataSources field expects to perform a verification using those data sources. 
        /// A failure to provide an element from the string collection will lead to a <a class='link-to-api' href='#errors'>1005</a> service error.
        /// </summary>
        /// <param name="packageID">Package ID</param>
        /// <param name="countryCode">Call CountryCodes to get the countries available to you.</param>
        public async Task<IEnumerable<string>> GetСonsentsAsync(string packageID, string countryCode)
        {
            var resourceParams = new List<string> { "consents", packageID, countryCode };
            var resource = new ResourceName(resourceParams);
            var response = await _context.GetAsync<IEnumerable<string>>(_configurationNamespace, resource).ConfigureAwait(false);
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
        public async Task<IEnumerable<Consent>> GetDetailedСonsentsAsync(string packageID, string countryCode)
        {
            var resourceParams = new List<string> { "consents", packageID, countryCode, "detail" };
            var resource = new ResourceName(resourceParams);
            var response = await _context.GetAsync<IEnumerable<Consent>>(_configurationNamespace, resource).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method retrieves all the country codes for given packageID
        /// </summary>
        /// <param name="packageID">Package ID</param>
        public async Task<IEnumerable<string>> GetCountryCodesAsync(string packageID)
        {
            var resourceParams = new List<string> { "countrycodes", packageID };
            var resource = new ResourceName(resourceParams);
            var response = await _context.GetAsync<IEnumerable<string>>(_configurationNamespace, resource).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Gets the provinces states or other subdivisions for a country, mostly matches ISO 3166-2
        /// </summary>
        /// /// <param name="countryCode">Country alpha2 code, get the the call to countrycodes</param>
        public async Task<IEnumerable<CountrySubdivision>> GetCountrySubdivisionsAsync(string countryCode)
        {
            var resource = new ResourceName("countrysubdivisions", countryCode);
            var response = await _context.GetAsync<IList<CountrySubdivision>>(_configurationNamespace, resource).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// This method retrieves all the fields codes for given packageID
        /// </summary>
        /// <param name="packageID">Call packageID to get the countries available to you.</param>
        /// <param name="countryCode">Call CountryCodes to get the countries available to you.</param>
        public async Task<Dictionary<string, dynamic>> GetFieldsAsync(string packageID, string countryCode)
        {
            var resourceParams = new List<string> { "fields", packageID, countryCode };
            var resource = new ResourceName(resourceParams);
            var response = await _context.GetAsync<Dictionary<string, dynamic>>(_configurationNamespace, resource).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Gets Recommended Fields based on Country and Package ID
        /// </summary>
        /// <param name="packageID">Package ID</param>
        /// <param name="countryCode">Call CountryCodes to get the countries available to you.</param>available to you.</param>
        /// <param name="configurationName">Identity Verification</param>
        public async Task<Dictionary<string, dynamic>> GetRecommendedFieldsAsync(string packageID, string countryCode)
        {
            var resourceParams = new List<string> { "fields", packageID, countryCode, "recommended" };
            var resource = new ResourceName(resourceParams);
            var response = await _context.GetAsync<Dictionary<string, dynamic>>(_configurationNamespace, resource).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Gets the test entities configured for your product and country.
        /// </summary>
        /// <param name="packageID"></param>
        /// <param name="countryCode">Call CountryCodes to get the countries available to you.</param>
        /// <returns></returns>
        public async Task<IEnumerable<TestEntityDataFields>> GetTestEntitiesAsync(string packageID, string countryCode)
        {
            var resourceParams = new List<string> { "testentities", packageID, countryCode };
            var resource = new ResourceName(resourceParams);
            var response = await _context.GetAsync<IList<TestEntityDataFields>>(_configurationNamespace, resource).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Gets datasource groups configured for your product and country.
        /// </summary>
        /// <param name="packageID">Package ID</param>
        /// <param name="countryCode">Call CountryCodes to get the countries available to you.</param>
        /// <returns></returns>
        public async Task<IEnumerable<NormalizedDatasourceGroupCountry>> GetDatasourcesAsync(string packageID, string countryCode)
        {
            var resourceParams = new List<string> { "datasources", packageID, countryCode };
            var resource = new ResourceName(resourceParams);
            var response = await _context.GetAsync<IList<NormalizedDatasourceGroupCountry>>(_configurationNamespace, resource).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Gets all datasource groups configured for your product.
        /// </summary>
        /// <param name="packageID">
        /// Package ID
        /// </param>
        /// <returns>  </returns>
        public async Task<List<NormalizedDatasourceGroupsWithCountry>> GetAllDatasourcesAsync(string packageID)
        {
            var resourceParams = new List<string> { "alldatasources", packageID };
            var resource = new ResourceName(resourceParams);
            var response = await _context.GetAsync<List<NormalizedDatasourceGroupsWithCountry>>(_configurationNamespace, resource).ConfigureAwait(false);
            return response;
        }

        #endregion
    }
}