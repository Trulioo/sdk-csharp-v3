using Trulioo.Client.V3.Products;

namespace Trulioo.Client.V3
{
    public interface ITruliooApiClient
    {
        #region Properties

        /// <summary>
        /// Gets the <see cref="TruliooBusiness"/> instance for this <see cref="ITruliooApiClient"/>.
        /// </summary>
        TruliooBusiness TruliooBusiness { get; }

        /// <summary>
        /// Gets the <see cref="Connection"/> instance for this <see cref="ITruliooApiClient"/>.
        /// </summary>
        Connection Connection { get; }
        /// <summary>
        /// Gets the <see cref="Verification"/> instance for this <see cref="ITruliooApiClient"/>.
        /// </summary>
        Verification Verification { get; }
        /// <summary>
        /// Gets the <see cref="Configuration"/> instance for this <see cref="ITruliooApiClient"/>.
        /// </summary>
        Configuration Configuration { get; }
        /// <summary>
        /// Gets the <see cref="V3.Products.Kyb"/> instance for this <see cref="ITruliooApiClient"/>.
        /// </summary>
        Kyb Kyb { get; }

        PersonFraud PersonFraud { get; }

        #endregion
    }
}
