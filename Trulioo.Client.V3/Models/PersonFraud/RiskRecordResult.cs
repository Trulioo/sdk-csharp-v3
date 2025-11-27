using System.Collections.Generic;

namespace Trulioo.Client.V3.Models.PersonFraud
{
    public class RiskRecordResult
    {
        public string TransactionRecordId { get; set; }
        public string UserID { get; set; }
        public List<PersonFraudServiceError> Errors { get; set; }
        public NormalizedRiskScore Score { get; set; }
        public NormalizedRiskEmail Email { get; set; }
        public NormalizedRiskPhone Phone { get; set; }
        public NormalizedRiskIP IP { get; set; }
        public NormalizedRiskFeatures Features { get; set; }
        public NormalizedRiskReference Reference { get; set; }
    }
}
