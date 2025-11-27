using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using Trulioo.Client.V3.Enums;
using Trulioo.Client.V3.Models.Fields;
using Trulioo.Client.V3.Models.PersonFraud;

namespace Trulioo.Client.V3.Models.Risk
{
    public class RiskVerifyRequest
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public VerificationType VerificationType { get; set; }

        public string PackageId { get; set; }

        public string CallBackUrl { get; set; }

        public int? Timeout { get; set; }

        public bool CleansedAddress { get; set; }

        public string[] ConsentForDataSources { get; set; }

        public string CountryCode { get; set; }

        public string CustomerReferenceID { get; set; }

        public DataFields DataFields { get; set; }

        public IEnumerable<PersonFraudServiceError> RequestErrors { get; set; }

        public bool VerboseMode { get; set; }

        public string BatchRecordID { get; set; }

        public ICollection<Metadata> RequestMetadata { get; set; }

        public class Metadata
        {
            public string Channel { get; set; }

            public string Value { get; set; }
        }
    }
}
