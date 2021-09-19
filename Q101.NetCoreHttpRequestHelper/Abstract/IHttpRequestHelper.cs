using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Q101.NetCoreHttpRequestHelper.Enums;

namespace Q101.NetCoreHttpRequestHelper.Abstract
{
    /// <summary>
    /// Http request helper
    /// </summary>
    public interface IHttpRequestHelper
    {
        /// <summary>
        /// Using serialization by camel case
        /// </summary>
        bool UseCamelCase { get; set; }

        /// <summary>
        /// Default http request headers
        /// </summary>
        Dictionary<string, string> DefaultHeaders { get; set; }

        /// <summary>
        /// Send request GET method
        /// </summary>
        public IHttpRequestSpecificTypeSender Get { get; }

        /// <summary>
        /// Send request POST method
        /// </summary>
        public IHttpRequestSpecificTypeSender Post { get; }

        /// <summary>
        /// Send request PUT method
        /// </summary>
        public IHttpRequestSpecificTypeSender Put { get; }

        /// <summary>
        /// Send request DELETE method
        /// </summary>
        public IHttpRequestSpecificTypeSender Delete { get; }

        /// <summary>
        /// Send request PATCH method
        /// </summary>
        public IHttpRequestSpecificTypeSender Patch { get; }

        /// <summary>
        /// Set Basic Authorization credentials.
        /// </summary>
        /// <param name="login">User name</param>
        /// <param name="password">password</param>
        void SetAuthorizationHeader(string login, string password);

        /// <summary>
        /// Set bearer token header Authorization.
        /// </summary>
        /// <param name="token">Auth token.</param>
        void SetAuthorizationHeader(string token);

        /// <summary>
        /// Send http request
        /// </summary>
        /// <typeparam name="T">Request body object type.</typeparam>
        /// <param name="contentType">Request body content type.</param>
        /// <param name="method">Http request method.</param>
        /// <param name="url">Http request url.</param>
        /// <param name="body">Http request body.</param>
        /// <param name="encoding">Http request body content encoding.</param>
        /// <param name="headers">Additional http request headers.</param>
        /// <returns></returns>
        Task<T> SendRequestAsync<T>(ContentTypes contentType,
                                    HttpMethod method,
                                    string url,
                                    object body = null,
                                    Encoding encoding = null,
                                    Dictionary<string, string> headers = null);
    }
}
