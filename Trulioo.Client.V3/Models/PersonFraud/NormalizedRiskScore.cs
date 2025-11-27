namespace Trulioo.Client.V3.Models.PersonFraud
{
    public class NormalizedRiskScore
    {
        public string ScoreType { get; set; }
        public NormalizedRiskReference ReasonCodes { get; set; }
        public string RiskLevel { get; set; }
        public int? Score { get; set; }
    }
    public class NormalizedRiskEmailInfo
    {
        public string EmailValid { get; set; }
        public bool? EmailIsDisposable { get; set; }
        public string EmailType { get; set; }
        public string EmailDomainCreationDate { get; set; }
        public string EmailDomainRegisteredTo { get; set; }
        public bool IsRandomlyGenerated { get; set; }
        public bool EmailHandleNameApplicantNameMatch { get; set; }
        public string EmailOrganizationType { get; set; }
    }
    public class NormalizedRiskEmailOnlineAccounts
    {
        public int EmailNetworkingAccountsCount { get; set; }
        public int EmailEcommerceAccountsCount { get; set; }
        public int EmailMessagingAccountsCount { get; set; }
        public int EmailOtherAccountsCount { get; set; }
        public int EmailAllAccountCount { get; set; }
    }
    public class NormalizedRiskEmail
    {
        public string EmailFirstSeen { get; set; }
        public string EmailToName { get; set; }
        public NormalizedRiskEmailInfo EmailInfo { get; set; }
        public NormalizedRiskEmailOnlineAccounts EmailOnlineAccounts { get; set; }
        public int? EmailDataBreachCount { get; set; }
    }
    public class NormalizedRiskPhoneInfo
    {
        public bool? PhoneValid { get; set; }
        public string PhoneCarrier { get; set; }
        public bool PhoneIsDisposable { get; set; }
        public string PhoneDisposableProviderName { get; set; }
        public string LastPortDate { get; set; }
        public bool? IsBusiness { get; set; }
    }

    public class NormalizedRiskPhoneOnlineAccounts
    {
        public int PhoneNetworkingAccountsCount { get; set; }
        public int PhoneEcommerceAccountsCount { get; set; }
        public int PhonePlatformAccountsCount { get; set; }
        public int PhoneOtherAccountsCount { get; set; }
        public int PhoneCallingAccountsCount { get; set; }
        public int PhoneAllAccountsCount { get; set; }
    }
    public class NormalizedRiskPhone
    {
        public string PhoneToName { get; set; }
        public string PhoneLineType { get; set; }
        public int? PhoneDataBreachCount { get; set; }
        public NormalizedRiskPhoneInfo PhoneInfo { get; set; }
        public NormalizedRiskPhoneOnlineAccounts PhoneOnlineAccounts { get; set; }
    }
    public class NormalizedRiskIPInfo
    {
        public string ISPName { get; set; }
        public string IPConnectionType { get; set; }
        public string IPCompanyDomain { get; set; }
        public string IPCompanyName { get; set; }
    }
    public class NormalizedRiskIPGeo
    {
        public string IPCountry { get; set; }
        public string IPCity { get; set; }
        public string IPPostalCode { get; set; }
        public string IPLat { get; set; }
        public string IPLong { get; set; }
        public int? IPDeviceTimeZoneDistance { get; set; }
        public int? IPToAddressDistance { get; set; }
        public int? IPToPhoneDistance { get; set; }
    }
    public class NormalizedRiskIPCategories
    {
        public bool? Proxy { get; set; }
        public string ProxyType { get; set; }
        public bool Tor { get; set; }
        public bool VPN { get; set; }
        public int BlacklistCount { get; set; }
        public bool CloudHost { get; set; }
        public bool? IsRelay { get; set; }
    }
    public class NormalizedRiskIP
    {
        public NormalizedRiskIPInfo IPInfo { get; set; }
        public NormalizedRiskIPGeo IPGeo { get; set; }
        public NormalizedRiskIPCategories IPCategories { get; set; }
    }
    public class NormalizedRiskVelocity
    {
        public int? VelocityEmailToDistinctPerson { get; set; }
        public int? VelocityPhoneToDistinctPerson { get; set; }
        public int? VelocityIPToDistinctPerson { get; set; }
        public int? VelocityAddressToDistinctPerson { get; set; }
    }
    public class NormalizedRiskFraudLinkages
    {
        public bool? NegAddress { get; set; }
        public bool? NegPhone { get; set; }
        public bool? NegEmail { get; set; }
        public bool? NegIP { get; set; }
    }
    public class NormalizedRiskFeatures
    {
        public NormalizedRiskVelocity Velocity { get; set; }
        public NormalizedRiskFraudLinkages FraudLinkages { get; set; }
    }
    public class NormalizedRiskReference
    {
        public bool? PhoneToIPCountry { get; set; }
        public bool? EmailAndPhoneLinked { get; set; }
        public bool? EmailToPhoneProfileImage { get; set; }
        public bool NameAnomalous { get; set; }
    }
}
