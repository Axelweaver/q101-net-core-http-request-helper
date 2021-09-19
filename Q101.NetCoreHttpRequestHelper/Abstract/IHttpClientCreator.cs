using System.Collections.Generic;
using System.Net.Http;

namespace Q101.NetCoreHttpRequestHelper.Abstract
{
    /// <summary>
    /// HttpClientCreator
    /// </summary>
    public interface IHttpClientCreator
    {
        /// <summary>
        /// Default http request headers.
        /// </summary>
        static Dictionary<string, string> DefaultHeaders { get; set; }

        /// <summary>
        /// Create http client.
        /// </summary>
        /// <returns>Http client.</returns>
        HttpClient Create();

        /// <summary>
        /// Set Basic Authorization credentials.
        /// </summary>
        /// <param name="login">User name.</param>
        /// <param name="password">password.</param>
        void SetAuthorizationHeader(string login, string password);

        /// <summary>
        /// Set bearer token header Authorization.
        /// </summary>
        /// <param name="token">Auth token.</param>
        void SetAuthorizationHeader(string token);
    }
}
