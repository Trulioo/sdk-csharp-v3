using System;

namespace Trulioo.Client.V3.Models.Account
{
    public class PackageResult
    {
        public int ID { get; set; }
        public string ExternalPackageName { get; set; }
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string PackageIdentifier { get; set; }
        public int AccountID { get; set; }
        public string AccountIdentifier { get; set; }
        public string Name { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreationTime { get; set; }
    }
}