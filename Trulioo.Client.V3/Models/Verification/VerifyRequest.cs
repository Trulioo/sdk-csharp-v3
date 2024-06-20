using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using Trulioo.Client.V3.Enums;
using Trulioo.Client.V3.Models.Errors;
using Trulioo.Client.V3.Models.Fields;

namespace Trulioo.Client.V3.Models.Verification
{
    /// <summary>
    /// The verification call for the Normalized API is a POST method call
    /// </summary>
    public class VerifyRequest
    {
        /// <summary>
        ///  The type of verification to perform.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public VerificationType VerificationType { get; set; }

        /// <summary>
        ///  The Package ID to run the transaction under
        /// </summary>
        public string PackageId { get; set; }

        /// <summary>
        /// If set, the transaction will run asynchronously and Trulioo will try to update the client with transaction state updates until completed.
        /// </summary>
        public string CallBackUrl { get; set; }

        /// <summary>
        /// If set, Trulioo will try to update the client synchronously within the timeout in seconds. If failed to accomplish, the transaction will be canceled.
        /// </summary>
        public int? Timeout { get; set; }

        /// <summary>
        /// Set to true if you want to receive address cleanse information,
        /// This will only change the response if you have address cleansing enabled for the country you are querying for.
        /// </summary>
        public bool CleansedAddress { get; set; }

        /// <summary>
        /// Some datasources required your customer provide consent to access them.  Set that the customer has provided consent for each
        /// datasource that requires one.  If consent is not provided the datasource will not be queried.
        /// The consent for the data sources which will be interrogated as a part of the request.
        /// Included only for the data sources which explicitly require consent
        /// </summary>
        public string[] ConsentForDataSources { get; set; }

        /// <summary>
        /// The country code for which the verification needs to be performed.
        /// Two-letter alpha code for the country for which the verification needs to be performed. 
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Used to send a Customer Reference ID which will be returned back in the response
        /// </summary>
        public string CustomerReferenceID { get; set; }

        /// <summary>
        /// The data field name-value pairs for the data elements on which the verification is to be performed
        /// </summary>
        public DataFields DataFields { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ServiceError> RequestErrors { get; set; }

        /// <summary>
        /// Verbose Mode output flag. Default value is false.
        /// </summary>
        public bool VerboseMode { get; set; }

        /// <summary>
        /// Correlation ID for running batches
        /// </summary>
        public string BatchRecordID { get; set; }

        public ICollection<Metadata> RequestMetadata { get; set; }

        public class Metadata
        {
            /// <summary>
            /// 
            /// </summary>
            /// <value>Metadata Channel.</value>
            public string Channel { get; set; }

            /// <summary>
            /// 
            /// </summary>
            /// <value>Metadata Value.</value>
            public string Value { get; set; }
        }
    }
}
