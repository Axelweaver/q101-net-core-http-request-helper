using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Q101.NetCoreHttpRequestHelper.Abstract;

namespace Q101.NetCoreHttpRequestHelper.Concrete
{
    /// <inheritdoc />
    public class HttpClientCreator : IHttpClientCreator
    {
        // disable ssl check
        private readonly bool _disableSslCheck;

        // authentication http request headers
        private AuthenticationHeaderValue _authenticationHeader;

        public static Dictionary<string, string> DefaultHeaders { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="disableSslCheck"></param>
        public HttpClientCreator(bool disableSslCheck)
        {
            _disableSslCheck = disableSslCheck;
        }

        /// <inheritdoc />
        public void SetAuthorizationHeader(string login, string password)
        {
            _authenticationHeader = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes($"{login}:{password}")));
        }

        /// <inheritdoc />
        public void SetAuthorizationHeader(string token)
        {
            _authenticationHeader = new AuthenticationHeaderValue("Token", token);
        }

        /// <inheritdoc />
        public HttpClient Create()
        {

            var httpHandler = new HttpClientHandler();

            if (_disableSslCheck)
            {
                httpHandler.ServerCertificateCustomValidationCallback =
                    (sender, cert, chain, sslPolicyErrors) => true;
            }

            var httpClient = new HttpClient(httpHandler);

            if (_authenticationHeader != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = _authenticationHeader;
            }

            if (DefaultHeaders == null)
            {
                return httpClient;
            }

            foreach (var pair in DefaultHeaders)
            {
                httpClient.DefaultRequestHeaders.Add(pair.Key, pair.Value);
            }

            return httpClient;
        }
    }
}
