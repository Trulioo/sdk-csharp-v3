using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trulioo.Client.V3.Models.Business;
using Trulioo.Client.V3.Models.Verification;
using Trulioo.Client.V3.URI;

namespace Trulioo.Client.V3
{
    /// <summary>
    /// Provides a class for working with Trulioo Business.
    /// </summary>
    [Obsolete("This class is deprecated. Use Kyb instead.")]
    public class TruliooBusiness
    {
        #region Private Properties

        private TruliooApiClient _service;
        private readonly Namespace _businessNamespace = new Namespace("v3/business");

        private Context _context
        {
            get { return _service.Context; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TruliooBusiness"/> class.
        /// </summary>
        /// <param name="service">
        /// An object representing the root of Trulioo configuration service.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="service"/> is <c>null</c>.
        /// </exception>
        protected internal TruliooBusiness(TruliooApiClient service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }


        #endregion

        #region Methods

        /// <summary>
        /// Business search call for Trulioo API Client V3
        /// </summary>
        /// <param name="request"> Request object containing parameters to search for </param>
        /// <returns> List of possible businesses from search </returns>
        [Obsolete("This class is deprecated. Use Kyb.BusinessSearchAsync() instead.")]
        public async Task<BusinessSearchResponse> BusinessSearchAsync(BusinessSearchRequest request)
        {
            var resource = new ResourceName("search");
            var response = await _context.PostAsync<BusinessSearchResponse>(_businessNamespace, resource, request).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Business verification call for Trulioo API Client V3
        /// </summary>
        /// <param name="request"> Request object containing parameters to verify business </param>
        /// <returns> Verification results </returns>
        [Obsolete("This class is deprecated. Use Kyb.BusinessVerifyAsync() instead.")]
        public async Task<VerifyResult> BusinessVerifyAsync(BusinessVerifyRequest request)
        {
            var resource = new ResourceName("verify");
            var response = await _context.PostAsync<VerifyResult>(_businessNamespace, resource, request).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Gets all jurisdictions of incorporation for all countries if no country is supplied.
        /// Gets the jurisdictions of incorporation for a country, if country is supplied.
        /// </summary>
        /// <param name="countryCode"> Optional country alpha2 code </param>
        /// <returns>  </returns>
        [Obsolete("This class is deprecated. Use Kyb.GetCountryJOIAsync() instead.")]
        public async Task<IEnumerable<CountrySubdivision>> GetCountryJOIAsync(string countryCode = null)
        {
            var resourceParams = new List<string> {"countryJOI", countryCode}.Where(x => !string.IsNullOrWhiteSpace(x));
            var resource = new ResourceName(resourceParams);
            var response = await _context.GetAsync<IEnumerable<CountrySubdivision>>(_businessNamespace, resource).ConfigureAwait(false);
            return response;
        }

         /// <summary>
        /// Gets the currently configured business registration numbers, for optionally supplied country and jurisdiction
        /// A country must be supplied in order to use a jurisdiction.
        /// </summary>
        /// <param name="countryCode"> Optional country alpha2 code </param>
        /// <param name="jurisdictionCode"> Optional jurisdiction code </param>
        /// <returns>  </returns>
        [Obsolete("This class is deprecated. Use Kyb.GetBusinessRegistrationNumbersAsync() instead.")]
        public async Task<IEnumerable<BusinessRegistrationNumber>> GetBusinessRegistrationNumbersAsync(string countryCode = null, string jurisdictionCode = null)
        {
            if (string.IsNullOrWhiteSpace(countryCode) && !string.IsNullOrWhiteSpace(jurisdictionCode))
            {
                throw new ArgumentException("Cannot use jurisdiction without a country.");
            }

            var resourceParams = new List<string> {"businessregistrationnumbers", countryCode, jurisdictionCode}.Where(x => !string.IsNullOrWhiteSpace(x));
            var resource = new ResourceName(resourceParams);
            var response = await _context.GetAsync<IEnumerable<BusinessRegistrationNumber>>(_businessNamespace, resource).ConfigureAwait(false);
            return response;
        }

        #endregion
    }
}
