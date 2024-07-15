# Trulioo SDK for C# Changelog


## Version 1.0.11.0
Fix typo in download document filename

## Version 1.0.10.0
Added new Configuration functions
	- GetСonsentsAsync
	- GetDetailedСonsentsAsync
	- GetCountryCodesAsync
	- GetCountrySubdivisionsAsync
	- GetFieldsAsync
	- GetRecommendedFieldsAsync
	- GetTestEntitiesAsync
	- GetDatasourcesAsync
Add BatchRecordID to VerifyRequest and BusinessVerifyRequest

## Version 1.0.9.0
Added new document types
	- CompletePlus
	- ArticleOfAuthority
	- AgentAddressChange

## Version 1.0.6.0
Added new document types
	- RegisterReport
	- CreditCheck
	- CreditReport
	- GISAExtract
	- VRExtract
	- RegisterCheck
	- TradeRegisterReport
	- BeneficialOwnersCheck
	- AnnualAccounts
	- FiledChanges
	- FiledDocuments

## Version 1.0.4.0
Serialize VerificationType of VerifyRequest as a string
Timeout of VerifyRequest is nullable
Change input type of BusinessVerifyAsync to be BusinessVerifyRequest
Add SearchType to BusinessSearchRequest

## Version 1.0.3.0
Add Configuration
Add GetAllDatasourcesAsync to Configuration
Add Configuration unit test

## Version 1.0.2.0

Add CountryCode field to CountrySubdivision.cs

## Version 1.0.1.0

Initial Build of Trulioo SDK v3 for C#

