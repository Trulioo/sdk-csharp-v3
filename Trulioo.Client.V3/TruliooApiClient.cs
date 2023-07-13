using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trulioo.Client.V3
{
    public class TruliooApiClient : ITruliooApiClient, IContextAware, IDisposable
    {
        #region Properties

        /// <summary>
        /// Gets the <see cref="Context"/> instance for this <see cref="TruliooApiClient"/>.
        /// </summary>
        public Context Context { get; }

        /// <summary>
        /// Gets the <see cref="TruliooBusiness"/> instance for this <see cref="TruliooApiClient"/>.
        /// </summary>
        public TruliooBusiness TruliooBusiness { get; }

        /// <summary>
        /// Gets the <see cref="Connection"/> instance for this <see cref="TruliooApiClient"/>.
        /// </summary>
        public Connection Connection { get; }

        /// <summary>
        /// Gets the <see cref="Verification"/> instance for this <see cref="TruliooApiClient"/>.
        /// </summary>
        public Verification Verification { get; }

        /// <summary>
        /// Gets the <see cref="Configuration"/> instance for this <see cref="TruliooApiClient"/>.
        /// </summary>
        public Configuration Configuration { get; }

        #endregion

        #region Private Fields

        private bool _disposed;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TruliooApiClient"/> class.
        /// </summary>
        /// <param name="context">
        /// The context for requests by the new <see cref="TruliooApiClient"/>.
        /// </param>
        /// ### <exception cref="ArgumentNullException">
        /// <paramref name="context"/> is <c>null</c>.
        /// </exception>
        public TruliooApiClient(Context context)
        {
            Context = context;
            TruliooBusiness = new TruliooBusiness(this);
            Verification = new Verification(this);
            Connection = new Connection(this);
            Configuration = new Configuration(this);
        }

        /// <summary>
        ///  Initializes a new instance of the <see cref="TruliooApiClient"/> class.
        /// </summary>
        /// <param name="clientId">
        ///  The client ID for the new <see cref="TruliooApiClient"/>.
        /// </param>
        /// <param name="clientSecret">
        ///  The client secret for the new <see cref="TruliooApiClient"/>.
        /// </param>
        /// ### <exception name="ArgumentException">
        /// <paramref name="clientId"/> is <c>null</c>.
        /// <paramref name="clientSecret"/> is <c>null</c>.
        /// </exception>
        public TruliooApiClient(string clientId, string clientSecret)
            : this(new Context(clientId, clientSecret))
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the credentials used for authentication of Trulioo API calls
        /// </summary>
        public async Task<bool> UpdateCredentialsAsync()
        {
            return await Context.UpdateCredentials().ConfigureAwait(false);
        }

        /// <summary>
        /// Releases all resources used by the <see cref="TruliooApiClient"/>.
        /// </summary>
        /// <remarks>
        /// Do not override this method. Override <see cref="TruliooApiClient.Dispose(bool)"/> instead.
        /// </remarks>
        /// <seealso cref="M:System.IDisposable.Dispose()"/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all resources used by the <see cref="TruliooApiClient"/>.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; 
        /// <c>false</c> to release only unmanaged resources.
        /// </param>
        /// <remarks>
        /// Subclasses should implement the disposable pattern as follows:
        /// <list type="bullet">
        /// <item><description>
        ///     Override this method and call it from the override.
        ///     </description></item>
        /// <item><description>
        ///     Provide a finalizer, if needed, and call this method from it.
        ///     </description></item>
        /// <item><description>
        ///     To help ensure that resources are always cleaned up appropriately,
        ///     ensure that the override is callable multiple times without throwing
        ///     an exception.
        ///     </description></item>
        /// </list>
        /// There is no performance benefit in overriding this method on types that
        /// use only managed resources (such as arrays) because they are
        /// automatically reclaimed by the garbage collector. See
        /// <a href="http://tiny.cc/8kzuzx">Implementing a Dispose Method</a>.
        /// </remarks>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;
                if (Context != null)
                {
                    Context.Dispose();
                }
            }
        }
        #endregion
    }
}
