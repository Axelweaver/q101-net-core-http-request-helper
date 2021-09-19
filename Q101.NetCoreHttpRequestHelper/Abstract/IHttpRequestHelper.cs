using System.Collections.Generic;

namespace Q101.NetCoreHttpRequestHelper.Abstract
{
    /// <summary>
    /// Http request helper
    /// </summary>
    public interface IHttpRequestHelper
    {
        /// <summary>
        /// Disable check SSL 
        /// </summary>
        bool DisableSslCheck { get; set; }

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
    }
}
