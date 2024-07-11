using Microsoft.Extensions.Configuration;

namespace Trulioo.Client.V3.Tests
{
    public static class BaseFact
    {
        private static readonly string _apiHost;
        private static readonly string _authHost;

        public static string ClientId { get; private set; }
        public static string ClientSecret { get; private set; }
        public static string KybClientId { get; private set; }
        public static string KybClientSecret { get; private set; }
        public static string PackageId { get; private set; }
        public static string CountryCode { get; private set; }
        public static string TransactionRecordId { get; private set; }
        public static string TransactionResult { get; private set; }
        public static string TransactionId { get; private set; }
        public static string TransactionStatus { get; private set; }

        static BaseFact()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("3ad72570-7ec6-448c-a736-1c5ea544ad0e")
                .Build();

            _apiHost = config["NapiHost"];
            _authHost = config["IdpHost"];

            ClientId = config["ClientId"];
            ClientSecret = config["ClientSecret"];
            KybClientId = config["KybClientId"];
            KybClientSecret = config["KybClientSecret"];

            PackageId = config["PackageId"];
            CountryCode = config["CountryCode"];

            TransactionRecordId = config["TransactionRecordId"];
            TransactionResult = config["Json_TransactionResult"];

            TransactionId = config["TransactionId"];
            TransactionStatus = config["Json_TransactionStatus"];
        }

        public static async Task<TruliooApiClient> GetTruliooClientAsync()
        {
            var context = new Context(ClientId, ClientSecret)
            {
                ApiHost = _apiHost,
                AuthHost = _authHost
            };
            var client = new TruliooApiClient(context);
            await client.UpdateCredentialsAsync();
            return client;
        }

        public static async Task<TruliooApiClient> GetMockedTruliooClientAsync(HttpClientHandler mockHandler)
        {
            var context = new Context(ClientId, ClientSecret, TimeSpan.FromMinutes(2), mockHandler)
            {
                ApiHost = _apiHost,
                AuthHost = _authHost
            };
            var client = new TruliooApiClient(context);
            await client.UpdateCredentialsAsync();
            return client;
        }

        public static async Task<TruliooApiClient> GetTruliooKYBClientAsync()
        {
            var context = new Context(KybClientId, KybClientSecret)
            {
                ApiHost = _apiHost,
                AuthHost = _authHost
            };
            var client = new TruliooApiClient(context);
            await client.UpdateCredentialsAsync();
            return client;
        }
    }
}
