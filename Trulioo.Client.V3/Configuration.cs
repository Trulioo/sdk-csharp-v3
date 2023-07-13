using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trulioo.Client.V3.Models.Configuration;
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
