using System;
using Trulioo.Client.V3.Models.PersonFraud;

namespace Trulioo.Client.V3.Models.Risk
{
    public class RiskVerifyResult
    {
        public string TransactionId { get; set; }
        public DateTime UploadedDt { get; set;  }
        public DateTime? CompletedDt { get; set;  }
        public string CountryCode { get; set; }
        public string ProductName { get; set; }
        public RiskRecordResult Record { get; set;  }
        public string CustomerReferenceID { get; set;  }
    }
}
