using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace Q101.NetCoreHttpRequestHelper
{
    /// <summary>
    /// Http request sending helper.
    /// </summary>
    public interface IHttpRequestHelper
    {
        /// <summary>
        /// HttpClient used by service.
        /// </summary>
        HttpClient HttpClient { get; }

        /// <summary>
        /// Helper request options.
        /// </summary>
        HttpRequestHelperOptions Options { get; }

        /// <summary>
        /// Add Authorization header to requests.
        /// </summary>
        /// <param name="value">Header value.</param>
        void UseAuthorizationHeader(string value);

        /// <summary>
        /// Add basic authorization header to requests.
        /// </summary>
        /// <param name="login">Basic auth login.</param>
        /// <param name="password">Basic auth password.</param>
        void UseBasicAuthorizationHeader(string login, string password);

        /// <summary>
        /// Add Token authorization header to requests.
        /// </summary>
        /// <param name="token">Token value.</param>
        void UseTokenAuthorizationHeader(string token);

        /// <summary>
        /// Add Bearer token authorization header to requests.
        /// </summary>
        /// <param name="token">Bearer token value.</param>
        void UseJwtTokenAuthorizationHeader(string token);

        /// <summary>
        /// Send request using JSON object content.
        /// </summary>
        public IHttpContentSender<object> Json { get; }

        /// <summary>
        /// Send request using XML object content.
        /// </summary>
        public IHttpContentSender<object> Xml { get; }

        /// <summary>
        /// Send request using stream content.
        /// </summary>
        public IHttpContentSender<Stream> Stream { get; }
    }
}
