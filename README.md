# Trulioo SDK v3 for C# #

## Version 1.0.2.0

The Trulioo Software Development Kit (SDK) v3 for C# contains library code designed to enable developers to customize the integration of GlobalGateway into your automated business processes or website.

Trulioo is a leading global ID verification company providing advanced analytics based on traditional information such as public records, credit files and government data as well as alternative sources including social login providers, ad networks, mobile applications, e-commerce websites and social networks. Trulioo specializes in scoring online identities as authentic, machine generated or fraudulent with our identity bureau covering 4 billion people in over 40 countries, including coverage for the most challenging demographics from emerging markets such as China, Russia, and Brazil.

Trulioo enables increased trust and safety online by powering fraud prevention and compliance systems for hundreds of clients including governments, eCommerce merchants, financial services, insurance, health, and travel companies.

Below is an example of initializing the Trulioo API client and test connection:

```csharp
using Trulioo.Client.V3;

var truliooClient = new TruliooApiClient("-- YOUR WEB SERVICE CLIENT ID --", "-- YOUR WEB SERVICE CLIENT SECRET --");

await truliooClient.UpdateCredentialsAsync();

var responseString = await truliooClient.Connection.TestAuthenticationAsync();

```

## Supported platforms
.NET Standard 2.0 

#### Development environment

##### Visual Studio
The Trulioo SDK v3 for C# supports development in [Microsoft Visual Studio](https://visualstudio.microsoft.com/).

### Documentation and resources

If you need to know more:

* For more about the Trulioo REST API V3, see the [REST API Reference](https://api.globaldatacompany.com/).

* For more about Trulioo in general, see [Trulioo Website](https://www.trulioo.com/).

### Contact Us

You can reach via channels listed on [Trulioo website](https://www.trulioo.com/company/contact-us/)

## License

The Trulioo SDK for C# is licensed under the Apache License 2.0. Details can be found in the LICENSE file.