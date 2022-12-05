using Q101.NetCoreHttpRequestHelper.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Q101.NetCoreHttpRequestHelper
{
    /// <summary>
    /// Http request sending helper.
    /// </summary>
    public class HttpRequestHelper : IHttpRequestHelper
    {
        #region private props

        /// <summary>
        /// JSON converter.
        /// </summary>
        private readonly IJsonConverter _jsonConverter;

        /// <summary>
        /// Xml converter.
        /// </summary>
        private readonly IXmlConverter _xmlConverter;

        /// <summary>
        /// Authentication header.
        /// </summary>
        private AuthenticationHeaderValue _authenticationHeader;

        /// <summary>
        /// Custom token Authentication header.
        /// </summary>
        private KeyValuePair<string, string> _customAuthorizationHeader;

        #endregion

        /// <inheritdoc />
        public HttpClient HttpClient { get; }

        /// <inheritdoc />
        public HttpRequestHelperOptions Options { get; }

        /// <inheritdoc />
        public IHttpContentSender<object> Json { get; }

        /// <inheritdoc />
        public IHttpContentSender<object> Xml { get; }

        /// <inheritdoc />
        public IHttpContentSender<Stream> Stream { get; }

        /// <inheritdoc />
        public IHttpContentSender<MultipartFormDataContent> FormData { get; }

        #region constructor

        /// <summary>
        /// Http request sending helper.
        /// </summary>
        public HttpRequestHelper(
            HttpClient httpClient,
            IJsonConverter jsonConverter,
            IXmlConverter xmlConverter)
        {
            HttpClient = httpClient;
            Options = new HttpRequestHelperOptions();

            _jsonConverter = jsonConverter;
            _xmlConverter = xmlConverter;

            Json = new HttpContentSender<object>(
                ConfigureHttpClient,
                ContentTypes.Json,
                Options,
                _jsonConverter,
                _xmlConverter);

            Xml = new HttpContentSender<object>(
                ConfigureHttpClient,
                ContentTypes.Xml,
                Options,
                _jsonConverter,
                _xmlConverter);

            Stream = new HttpContentSender<Stream>(
                ConfigureHttpClient,
                ContentTypes.Stream,
                Options,
                _jsonConverter,
                _xmlConverter);

            FormData = new HttpContentSender<MultipartFormDataContent>(
                ConfigureHttpClient,
                ContentTypes.FormData,
                Options,
                _jsonConverter,
                _xmlConverter);
        }

        #endregion

        /// <inheritdoc />
        public void UseAuthorizationHeader(string value)
        {
            _customAuthorizationHeader = new KeyValuePair<string, string>
                ("Authorization", value);
        }

        /// <inheritdoc />
        public void UseBasicAuthorizationHeader(string login, string password)
        {
            _authenticationHeader = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes($"{login}:{password}")));
        }

        /// <inheritdoc />
        public void UseTokenAuthorizationHeader(string token)
        {
            _authenticationHeader = new AuthenticationHeaderValue("Token", token);
        }

        /// <inheritdoc />
        public void UseJwtTokenAuthorizationHeader(string token)
        {
            _customAuthorizationHeader = new KeyValuePair<string, string>
                ("Authorization", $"Bearer {token}");
        }

        /// <summary>
        /// Apply helper configuration to HttpClient.
        /// </summary>
        private HttpClient ConfigureHttpClient()
        {
            if (_authenticationHeader != null)
            {
                HttpClient.DefaultRequestHeaders.Authorization = _authenticationHeader;
            }

            if (!string.IsNullOrEmpty(_customAuthorizationHeader.Value)
                && HttpClient.DefaultRequestHeaders.Authorization == null)
            {
                HttpClient.DefaultRequestHeaders.Remove(
                    _customAuthorizationHeader.Key);

                HttpClient.DefaultRequestHeaders.Add(
                    _customAuthorizationHeader.Key,
                    _customAuthorizationHeader.Value);
            }

            foreach (var pair in Options.DefaultHeaders)
            {
                // Can't add duplicate key.
                HttpClient.DefaultRequestHeaders.Remove(pair.Key);
                HttpClient.DefaultRequestHeaders.Add(pair.Key, pair.Value);
            }

            return HttpClient;
        }
    }
}
