using System;

namespace Trulioo.Client.V3.Products
{
    public abstract class ProductBase
    {
        protected Context Context { get; }
        protected TruliooApiClient Client { get; set; }

        protected ProductBase(TruliooApiClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
            Context = client.Context ?? throw new ArgumentNullException(nameof(client.Context));
        }
    }
}
