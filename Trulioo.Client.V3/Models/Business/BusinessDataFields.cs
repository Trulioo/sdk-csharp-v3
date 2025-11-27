using System.Collections.Generic;
using Trulioo.Client.V3.Models.Fields;

namespace Trulioo.Client.V3.Models.Business
{
    public class BusinessDataFields
    {
        /// <summary>
        /// Name of the business to be verified
        /// </summary>
        public string BusinessName { get; set; }
        
        /// <summary>
        /// Trade Style Name of the business to be verified
        /// </summary>
        public string TradestyleName { get; set; }
        
        /// <summary>
        /// Tax ID Number of the business to be verified
        /// </summary>
        public string TaxIDNumber { get; set; }
        
        /// <summary>
        /// Registration number of business to be verified
        /// </summary>   
        public string BusinessRegistrationNumber { get; set; }
        
        /// <summary>
        /// Day of incorporation of the business to be verified
        /// </summary>   
        public int DayOfIncorporation { get; set; }
        
        /// <summary>
        /// Month of incorporation of the business to be verified
        /// </summary>   
        public int MonthOfIncorporation { get; set; }
        
        /// <summary>
        /// Year of incorporation of the business to be verified
        /// </summary>   
        public int YearOfIncorporation { get; set; }
        
        /// <summary>
        /// Jurisdiction Of Incorporation of the business to be verified
        /// </summary>   
        public string JurisdictionOfIncorporation { get; set; }
        
        /// <summary>
        /// Whether or not to retrieve shareholderList document
        /// </summary>   
        public bool ShareholderListDocument { get; set; }
        
        /// <summary>
        /// Whether or not to retrieve financial information document
        /// </summary>   
        public bool FinancialInformationDocument { get; set; }
        
        /// <summary>
        /// Whether or not to retrieve enhanced profile
        /// </summary>
        public bool EnhancedProfile { get; set; }
        
        /// <summary>
        /// Duns Number
        /// </summary>
        public string DunsNumber { get; set; }
        
        /// <summary>
        /// Whether or not to retrieve entity detail
        /// </summary>
        public bool Entities { get; set; }
        
        /// <summary>
        /// A list of people to check for their matchstatus
        /// </summary>
        public List<PersonOfSignificantControl> PeopleOfSignificantControl { get; set; }
        
        /// <summary>
        /// Whether or not to retrieve filing detail
        /// </summary>
        public bool Filings { get; set; }
        
        /// <summary>
        /// Whether or not to retrieve article of association document
        /// </summary>
        public bool ArticleOfAssociation { get; set; }
        
        /// <summary>
        /// Whether or not to retrieve registration detail document
        /// </summary>
        public bool RegistrationDetails { get; set; }
        
        /// <summary>
        /// Whether or not to retrieve annual report document
        /// </summary>
        public bool AnnualReport { get; set; }

        /// <summary>
        /// Whether or not to retrieve register report document
        /// </summary>
        public bool RegisterReport { get; set; }

        /// <summary>
        /// Whether or not to retrieve credit check
        /// </summary>
        public bool CreditCheck { get; set; }

        /// <summary>
        /// Whether or not to retrieve credit report document
        /// </summary>
        public bool CreditReport { get; set; }

        /// <summary>
        /// Whether or not to retrieve GISA extract document
        /// </summary>
        public bool GISAExtract { get; set; }

        /// <summary>
        /// Whether or not to retrieve VR extract document
        /// </summary>
        public bool VRExtract { get; set; }

        /// <summary>
        /// Whether or not to retrieve register check document
        /// </summary>
        public bool RegisterCheck { get; set; }

        /// <summary>
        /// Whether or not to retrieve trade register report document
        /// </summary>
        public bool TradeRegisterReport { get; set; }

        /// <summary>
        /// Whether or not to retrieve beneficial owners check
        /// </summary>
        public bool BeneficialOwnersCheck { get; set; }

        /// <summary>
        /// Whether or not to retrieve annual accounts document
        /// </summary>
        public bool AnnualAccounts { get; set; }

        /// <summary>
        /// Whether or not to retrieve filed changes document
        /// </summary>
        public bool FiledChanges { get; set; }

        /// <summary>
        /// Whether or not to retrieve filed documents
        /// </summary>
        public bool FiledDocuments { get; set; }

        /// <summary>
        /// Whether or not to perform a complete plus transaction
        /// </summary>
        public bool CompletePlus { get; set; }

        /// <summary>
        /// Whether or not to retrieve article of authority document
        /// </summary>
        public bool ArticleOfAuthority { get; set; }

        /// <summary>
        /// Whether or not to retrieve agent address change document
        /// </summary>
        public bool AgentAddressChange { get; set; }

        /// <summary>
        /// Telephone number of the business to be verified
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// Whether or not to trigger business monitoring auto-enrollment
        /// </summary>
        public bool KYBMonitoringEnabled { get; set; }

        /// <summary>
        /// Fields used for business monitoring
        /// </summary>
        public string KYBMonitoringFields { get; set; }

        /// <summary>
        /// Business monitoring polling frequency
        /// </summary>
        public string KYBMonitoringFrequency { get; set; }

        /// <summary>
        /// Business monitoring result callback endpoint
        /// </summary>
        public string KYBMonitoringCallbackURL { get; set; }

        /// <summary>
        /// Whether or not to fetch business monitoring results
        /// </summary>
        public bool? KYBMonitoringHistory { get; set; }

        /// <summary>
        /// CountrySpecific fields
        /// {"Country" : {"Field1" : "Value",
        /// "Field2" : "Value"
        /// }}
        /// </summary>
        public CountrySpecific CountrySpecific { get; set; }

        /// <summary>
        /// Location Information
        /// </summary>
        public List<BusinessLocation> Location { get; set; }
    }
}