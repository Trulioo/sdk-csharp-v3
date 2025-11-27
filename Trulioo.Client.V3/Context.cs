using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Trulioo.Client.V3.Compressor;
using Trulioo.Client.V3.Exceptions;
using Trulioo.Client.V3.Models;
using Trulioo.Client.V3.Models.Errors;
using Trulioo.Client.V3.URI;

namespace Trulioo.Client.V3
{
    /// <summary>
    /// Provides a class for sending HTTP requests and receiving HTTP responses from a Trulioo server.
    /// </summary>
    /// <seealso cref="T:System.IDisposable"/>
    public class Context : IDisposable
    {
        #region Public Fields and Properties

        /// <summary>
        /// Gets the Trulioo API host name associated with the current <see cref= "Context"/>.
        /// </summary>
        /// <value>
        /// A Trulioo host name.
        /// </value>
        public string ApiHost { get; set; } = "api.trulioo.com";

        /// <summary>
        /// Gets the Trulioo authentication host name associated with the current <see cref= "Context"/>.
        /// </summary>
        /// <value>
        /// A Trulioo host name.
        /// </value>
        public string AuthHost { get; set; } = "auth-api.trulioo.com";

        public TruliooCredentials Credentials { get; set; }

        public TimeSpan UpdateBuffer = TimeSpan.FromMilliseconds(100);

        #endregion

        #region Private Fields and Properties

        private bool _disposed;
        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
        };

        private HttpClient HttpClient { get; }

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class with a host, protocol by default is HTTPS.
        /// </summary>
        /// <param name="clientId">Client ID for current context</param>
        /// <param name="clientSecret">Client secret for current context</param>
        /// <param name="timeout">The HTTP timeout. 100 seconds by default.</param>
        /// <exception name="ArgumentException">
        /// <paramref name="clientId"/> is <c>null</c> or empty.
        /// <paramref name="clientSecret"/> is <c>null</c> or empty.
        /// </exception>
        public Context(string clientId, string clientSecret, TimeSpan timeout = default)
            : this(clientId, clientSecret, timeout, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class with a host, protocol by default is HTTPS.
        /// </summary>
        /// <param name="clientId">Client ID for current context</param>
        /// <param name="clientSecret">Client secret for current context</param>
        /// <param name="timeout">The HTTP timeout. 100 seconds by default.</param>
        /// <param name="handler">
        /// The <see cref="HttpMessageHandler"/> responsible for processing the HTTP response messages, by default a <see cref="GZipDecompressionHandler"/> is used.
        /// </param>
        /// <param name="disposeHandler">
        /// <c>true</c> if the inner handler should be disposed of by Dispose,
        /// <c>false</c> if you intend to reuse the inner handler.
        /// </param>
        /// <exception name="ArgumentException">
        /// <paramref name="clientId"/> is <c>null</c> or empty.
        /// <paramref name="clientSecret"/> is <c>null</c> or empty.
        /// </exception>
        public Context(string clientId, string clientSecret, TimeSpan timeout, HttpMessageHandler handler, bool disposeHandler = true)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentException(nameof(clientId));
            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentException(nameof(clientSecret));

            Credentials = new TruliooCredentials { ClientId = clientId, ClientSecret = clientSecret };
            HttpClient = handler == null ? new HttpClient(new GZipDecompressionHandler(), disposeHandler) : new HttpClient(handler, disposeHandler);

            if (timeout != default)
                HttpClient.Timeout = timeout;

            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient.DefaultRequestHeaders.Add("User-Agent", "trulioo-sdk-csharp/3.0");
            HttpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
        }
        #endregion

        #region Methods

        internal async Task<bool> UpdateCredentials()
        {
            var response = await sendIdentificationAsync().ConfigureAwait(false);
            TokenResult tokenResult = null;

            try
            {
                tokenResult = JsonConvert.DeserializeObject<TokenResult>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            }
            catch
            {
                return false;
            }

            if (tokenResult == null)
                return false;

            Credentials.BearerTokenExpiresAt = DateTime.UtcNow.AddSeconds(tokenResult.ExpiresIn);
            Credentials.BearerToken = tokenResult.AccessToken;

            return true;
        }

        /// <summary>
        /// Sends a GET request as an asynchronous operation.
        /// </summary>
        /// <param name="ns">
        /// An object identifying a Trulioo services namespace.
        /// </param>
        /// <param name="resource">
        /// An object identifying a resource.
        /// </param>
        /// <param name="processResponse">
        /// A function to handle the returned message
        /// </param>
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation Token that can cancel asynchronous operation
        /// </param>
        /// <returns>
        /// The response to the GET request.
        /// </returns>
        internal async Task<TReturn> GetAsync<TReturn>(Namespace ns, ResourceName resource, Func<HttpResponseMessage, TReturn> processResponse = null, CancellationToken cancellationToken = default)
        {
            var response = await sendAsync<TReturn>(HttpMethod.Get, ns, resource, processResponse: processResponse, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Sends a GET request and returns the content copied into a MemoryStream as an asynchronous operation.
        /// The original <see cref="HttpResponseMessage"/> and its underlying stream are disposed internally.
        /// </summary>
        /// <param name="ns">
        /// An object identifying a Trulioo services namespace.
        /// </param>
        /// <param name="resource">
        /// An object identifying a resource.
        /// </param>
        /// <param name="queryParams">
        /// A dictionary of query parameters to be added to the URL.
        /// </param>
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation Token that can cancel asynchronous operation
        /// </param>
        /// <returns>
        /// The content of the response as a <see cref="MemoryStream"/>.
        /// </returns>
        internal async Task<Stream> GetStreamAsync(Namespace ns, ResourceName resource, Dictionary<string, string> queryParams = null, CancellationToken cancellationToken = default)
        {
            if (DateTime.UtcNow >= Credentials.BearerTokenExpiresAt.AddMilliseconds(UpdateBuffer.TotalMilliseconds))
            {
                await UpdateCredentials().ConfigureAwait(false);
            }

            var serviceUri = createServiceUriWithQueryParameter(ns, resource, queryParams);
            using (var request = new HttpRequestMessage(HttpMethod.Get, serviceUri)) 
            { 
                request.Headers.Add("Authorization", $"Bearer {Credentials.BearerToken}");

                using (var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        await throwRequestExceptionAsync(response).ConfigureAwait(false);
                    }

                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        var memoryStream = new MemoryStream();
                        await responseStream.CopyToAsync(memoryStream).ConfigureAwait(false);
                        memoryStream.Position = 0;
                        return memoryStream;
                    }
                }
            }
        }

        /// <summary>
        /// Sends a GET request with query parameters and processes the response as an asynchronous operation.
        /// </summary>
        /// <param name="ns">
        /// An object identifying a Trulioo services namespace.
        /// </param>
        /// <param name="resource">
        /// An object identifying a resource.
        /// </param>
        /// <param name="queryParams">
        /// A dictionary of query parameters to be added to the URL.
        /// </param>
        /// <param name="processResponse">
        /// A function to handle the returned message.
        /// </param>
        /// <returns>
        /// The response to the GET request after being processed.
        /// </returns>
        internal async Task<TReturn> GetAsyncWithQueryParams<TReturn>(Namespace ns, ResourceName resource, Dictionary<string, string> queryParams = null, Func<HttpResponseMessage, TReturn> processResponse = null, CancellationToken cancellationToken = default)
        {
            if (DateTime.UtcNow >= Credentials.BearerTokenExpiresAt.AddMilliseconds(UpdateBuffer.TotalMilliseconds))
            {
                await UpdateCredentials().ConfigureAwait(false);
            }

            var serviceUri = createServiceUriWithQueryParameter(ns, resource, queryParams);
            using (var request = new HttpRequestMessage(HttpMethod.Get, serviceUri))
            {
                request.Headers.Add("Authorization", $"Bearer {Credentials.BearerToken}");

                using (var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        await throwRequestExceptionAsync(response).ConfigureAwait(false);
                    }

                    if (processResponse != null)
                    {
                        return processResponse(response);
                    }

                    var rawMessage = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return typeof(TReturn) == typeof(string) ? (TReturn)(object)rawMessage : JsonConvert.DeserializeObject<TReturn>(rawMessage);
                }
            }
        }

        /// <summary>
        /// Sends a POST request as an asynchronous operation.
        /// </summary>
        /// <param name="ns">
        /// An object identifying a Trulioo services namespace.
        /// </param>
        /// <param name="resource">
        /// An object identifying a resource.
        /// </param>
        /// <param name="content">
        /// An object identifying the HTTP content. 
        /// </param>
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation Token that can cancel asynchronous operation
        /// </param>
        /// <returns>
        /// </returns>
        internal async Task PostAsync(Namespace ns, ResourceName resource, dynamic content = null, CancellationToken cancellationToken = default)
        {
            await sendAsync(HttpMethod.Post, ns, resource, content, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a POST request as an asynchronous operation.
        /// </summary>
        /// <param name="ns">
        /// An object identifying a Trulioo services namespace.
        /// </param>
        /// <param name="resource">
        /// An object identifying a resource.
        /// </param>
        /// <param name="content">
        /// An object identifying the HTTP content. 
        /// </param>
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation Token that can cancel asynchronous operation
        /// </param>
        /// <returns>
        /// The response to the POST request.
        /// </returns>
        internal async Task<TReturn> PostAsync<TReturn>(Namespace ns, ResourceName resource, dynamic content = null, CancellationToken cancellationToken = default)
        {
            var response = await sendAsync<TReturn>(HttpMethod.Post, ns, resource, content, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Sends a PUT request as an asynchronous operation.
        /// </summary>
        /// <param name="ns">
        /// An object identifying a Trulioo services namespace.
        /// </param>
        /// <param name="resource">
        /// An object identifying a resource.
        /// </param>
        /// <param name="content">
        /// An object identifying the HTTP content. 
        /// </param>
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation Token that can cancel asynchronous operation
        /// </param>
        /// <returns>
        /// </returns>
        internal async Task PutAsync(Namespace ns, ResourceName resource, dynamic content = null, CancellationToken cancellationToken = default)
        {
            await sendAsync(HttpMethod.Put, ns, resource, content, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a DELETE request as an asynchronous operation.
        /// </summary>
        /// <param name="ns">
        /// An object identifying a Trulioo services namespace.
        /// </param>
        /// <param name="resource">
        /// An object identifying a resource.
        /// </param>
        /// <param name="content">
        /// An object identifying the HTTP content. 
        /// </param>
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation Token that can cancel asynchronous operation
        /// </param>
        /// <returns>
        /// </returns>
        internal async Task DeleteAsync(Namespace ns, ResourceName resource, dynamic content = null, CancellationToken cancellationToken = default)
        {
            await sendAsync(HttpMethod.Delete, ns, resource, content, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Releases all disposable resources used by the current
        /// <see cref= "Context"/>.
        /// </summary>
        /// <remarks>
        /// Do not override this method. Override <see cref="Dispose(bool)"/> instead.
        /// </remarks>
        /// <seealso cref="M:System.IDisposable.Dispose()"/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all resources used by the <see cref="Context"/>.
        /// </summary>
        /// <remarks>
        /// Subclasses should implement the disposable pattern as follows:
        /// <list type="bullet">
        /// <item><description>
        ///   Override this method and call it from the override.
        /// </description></item>
        /// <item><description>
        ///   Provide a finalizer, if needed, and call this method from it.
        /// </description></item>
        /// <item><description>
        ///   To help ensure that resources are always cleaned up appropriately,
        ///   ensure that the override is callable multiple times without throwing an
        ///   exception.
        /// </description></item>
        /// </list>
        /// There is no performance benefit in overriding this method on types that
        /// use only managed resources (such as arrays) because they are
        /// automatically reclaimed by the garbage collector. See
        /// <a href="http://tiny.cc/8kzuzx">Implementing a Dispose Method</a>.
        /// </remarks>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release
        /// only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;
                HttpClient.Dispose();
            }
        }

        #endregion

        #region Internal Methods

        private Uri createServiceUri(string host, Namespace ns, ResourceName resource)
        {
            var builder = new StringBuilder("https://")
                .Append(host)
                .Append(ns.ToUriString())
                .Append("/")
                .Append(resource.ToUriString());

            var uri = new Uri(builder.ToString(), UriKind.Absolute);
            return uri;
        }

        private Uri createServiceUriWithQueryParameter(Namespace ns, ResourceName resource, Dictionary<string, string> queryParams)
        {
            var serviceUri = createServiceUri(ApiHost, ns, resource);

            if (queryParams != null && queryParams.Any())
            {
                var uriBuilder = new UriBuilder(serviceUri);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                foreach (var param in queryParams)
                {
                    query[param.Key] = param.Value;
                }
                uriBuilder.Query = query.ToString();
                serviceUri = uriBuilder.Uri;
            }

            return serviceUri;
        }

        private async Task<TReturn> sendAsync<TReturn>(HttpMethod httpMethod, Namespace ns, ResourceName resource,
            dynamic content = null, Func<HttpResponseMessage, TReturn> processResponse = null, CancellationToken cancellationToken = default)
        {
            if (DateTime.UtcNow >= Credentials.BearerTokenExpiresAt.AddMilliseconds(UpdateBuffer.TotalMilliseconds))
            {
                await UpdateCredentials().ConfigureAwait(false);
            }

            var response = await sendInternalAsync(httpMethod, ns, resource, content, cancellationToken).ConfigureAwait(false);
            if (processResponse != null)
            {
                return processResponse(response);
            }
            
            var rawMessage = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return typeof(TReturn) == typeof(string) ? rawMessage : JsonConvert.DeserializeObject<TReturn>(rawMessage);
        }

        private async Task<HttpResponseMessage> sendInternalAsync(HttpMethod httpMethod, Namespace ns, ResourceName resource, dynamic content = null, CancellationToken cancellationToken = default)
        {
            var serviceUri = createServiceUri(ApiHost, ns, resource);
            var stringContent = getStringContent(content);

            using (var request = new HttpRequestMessage(httpMethod, serviceUri) { Content = stringContent })
            {
                request.Headers.Add("Authorization", $"Bearer {Credentials.BearerToken}");
                
                var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    await throwRequestExceptionAsync(response).ConfigureAwait(false);
                }
                return response;
            }
        }

        private async Task<HttpResponseMessage> sendIdentificationAsync()
        {
            var serviceUri = createServiceUri(AuthHost, new Namespace("connect"), new ResourceName("token"));
            var requestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", Credentials.ClientId),
                new KeyValuePair<string, string>("client_secret", Credentials.ClientSecret),
                new KeyValuePair<string, string>("scope", "napi.api"),
            });

            using (var request = new HttpRequestMessage(HttpMethod.Post, serviceUri) { Content = requestContent })
            {
                var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, CancellationToken.None).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    await throwRequestExceptionAsync(response).ConfigureAwait(false);
                }
                return response;
            }
            
        }

        /// <summary>
        /// Throw request exception asynchronous.
        /// </summary>
        /// <exception cref="RequestException">
        /// Thrown when a Request error condition occurs.
        /// </exception>
        /// <returns>
        /// A <see cref="Task"/> representing the operation.
        /// </returns>
        private static async Task throwRequestExceptionAsync(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var error = parseError(response.StatusCode, content);

            RequestException requestException;
            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    requestException = new BadRequestException(error.Message, error.Code, error.Reason);
                    break;
                case HttpStatusCode.Forbidden:
                    requestException = new Exceptions.UnauthorizedAccessException(error.Message, error.Code, error.Reason);
                    break;
                case HttpStatusCode.InternalServerError:
                    requestException = new InternalServerErrorException(error.Message, error.Code, error.Reason);
                    break;
                case HttpStatusCode.NotFound:
                    requestException = new ResourceNotFoundException(error.Message, error.Code, error.Reason);
                    break;
                case HttpStatusCode.Unauthorized:
                    requestException = new AuthenticationFailureException(error.Message, error.Code, error.Reason);
                    break;
                default:
                    requestException = new RequestException(error.Message, error.Code, error.Reason);
                    break;
            }
            throw requestException;
        }

        private static Error parseError(HttpStatusCode statusCode, string content)
        {
            Error error;
            try
            {
                error = JsonConvert.DeserializeObject<Error>(content) ?? new Error
                {
                    Code = (int)statusCode,
                    Message = string.IsNullOrEmpty(content) ? statusCode.ToString() : content
                };
            }
            catch (Exception ex)
            {
                error = new Error
                {
                    Code = (int)statusCode,
                    Message = string.IsNullOrEmpty(content) ? statusCode.ToString() : content,
                    Reason = ex.Message
                };
            }
            return error;
        }

        private static StringContent getStringContent(dynamic content)
        {
            if (object.ReferenceEquals(content, null))
                return null;
            return new StringContent(JsonConvert.SerializeObject(content, _jsonSerializerSettings), Encoding.UTF8, "application/json");
        }

        #endregion

    }
}
