using Q101.NetCoreHttpRequestHelper.Constants;
using Q101.NetCoreHttpRequestHelper.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Q101.NetCoreHttpRequestHelper
{
    /// <summary>
    /// HttpRequestHelper formatted content sender.
    /// </summary>
    internal class HttpContentSender<TContent> : IHttpContentSender<TContent>
    {
        #region private props

        /// <summary>
        /// HttpClient instance.
        /// </summary>
        private readonly Func<HttpClient> _httpClientProviderFunc;

        /// <summary>
        /// Sender content type.
        /// </summary>
        private readonly ContentTypes _contentType;

        /// <summary>
        /// Helper request options.
        /// </summary>
        private readonly HttpRequestHelperOptions _requestOptions;

        /// <summary>
        /// JSON converter.
        /// </summary>
        private readonly IJsonConverter _jsonConverter;

        /// <summary>
        /// Xml converter.
        /// </summary>
        private readonly IXmlConverter _xmlConverter;

        #endregion

        /// <summary>
        /// HttpRequestHelper formatted content sender.
        /// </summary>
        public HttpContentSender(
            Func<HttpClient> clientProviderFunc,
            ContentTypes contentType,
            HttpRequestHelperOptions requestOptions,
            IJsonConverter jsonConverter,
            IXmlConverter xmlConverter)
        {
            _httpClientProviderFunc = clientProviderFunc;
            _contentType = contentType;
            _requestOptions = requestOptions;
            _jsonConverter = jsonConverter;
            _xmlConverter = xmlConverter;
        }

        /// <inheritdoc/>
        public async Task<IHttpRequestHelperResponse> GetAsync(
            string url,
            TContent content = default,
            Dictionary<string, string> headers = null,
            Encoding encoding = null,
            bool ensureSuccess = true)
        {
            return await SendAsync(HttpMethod.Get, url, content, headers, encoding, ensureSuccess);
        }

        /// <inheritdoc/>
        public async Task<IHttpRequestHelperResponse> PostAsync(
            string url,
            TContent content = default,
            Dictionary<string, string> headers = null,
            Encoding encoding = null,
            bool ensureSuccess = true)
        {
            return await SendAsync(HttpMethod.Post, url, content, headers, encoding, ensureSuccess);
        }

        /// <inheritdoc/>
        public async Task<IHttpRequestHelperResponse> PutAsync(
            string url,
            TContent content = default,
            Dictionary<string, string> headers = null,
            Encoding encoding = null,
            bool ensureSuccess = true)
        {
            return await SendAsync(HttpMethod.Put, url, content, headers, encoding, ensureSuccess);
        }

        /// <inheritdoc/>
        public async Task<IHttpRequestHelperResponse> DeleteAsync(
            string url,
            TContent content = default,
            Dictionary<string, string> headers = null,
            Encoding encoding = null,
            bool ensureSuccess = true)
        {
            return await SendAsync(HttpMethod.Delete, url, content, headers, encoding, ensureSuccess);
        }

        /// <inheritdoc/>
        public async Task<IHttpRequestHelperResponse> PatchAsync(
            string url,
            TContent content = default,
            Dictionary<string, string> headers = null,
            Encoding encoding = null,
            bool ensureSuccess = true)
        {
            return await SendAsync(HttpMethod.Patch, url, content, headers, encoding, ensureSuccess);
        }

        /// <inheritdoc/>
        public async Task<IHttpRequestHelperResponse> SendAsync(
            HttpMethod method,
            string url,
            TContent content = default,
            Dictionary<string, string> headers = null,
            Encoding encoding = null,
            bool ensureSuccess = true)
        {
            var httpClient = _httpClientProviderFunc();
            var message = GetRequestMessage(method, url, content, headers, encoding);

            var response = await httpClient.SendAsync(message);

            if (ensureSuccess)
            {
                response.EnsureSuccessStatusCode();
            }

            var responseWrapper = new HttpRequestHelperResponse(
                response,
                _requestOptions.UseCamelCase,
                _jsonConverter,
                _xmlConverter);

            return responseWrapper;
        }

        /// <summary>
        /// Get HttpRequestMessage instance.
        /// </summary>
        /// <param name="method">Request method.</param>
        /// <param name="url">Request URL.</param>
        /// <param name="content">Request content.</param>
        /// <param name="headers">Request additional headers.</param>
        /// <param name="encoding">Request encoding (default UTF-8).</param>
        /// <returns>Configured HttpRequestMessage.</returns>
        private HttpRequestMessage GetRequestMessage(
            HttpMethod method,
            string url,
            TContent content = default,
            Dictionary<string, string> headers = null,
            Encoding encoding = null)
        {
            encoding ??= _requestOptions.DefaultEncoding;

            var requestMessage = new HttpRequestMessage(method, url);

            // Configure content.
            if (content != null)
            {
                switch (_contentType)
                {
                    case ContentTypes.Json:
                        {
                            var json = _requestOptions.UseCamelCase
                                ? _jsonConverter.SerializeCamelCase(content)
                                : _jsonConverter.Serialize(content);

                            requestMessage.Content = new StringContent(
                                json, encoding, ContentTypeStrings.Json);

                            break;
                        }
                    case ContentTypes.Xml:
                        {
                            var xml = _xmlConverter.Serialize(content, encoding);

                            requestMessage.Content = new StringContent(
                                xml, encoding, ContentTypeStrings.Xml);

                            break;
                        }
                    case ContentTypes.Stream:
                        {
                            requestMessage.Content = new StreamContent(content as Stream);

                            break;
                        }
                }
            }

            if (headers != null)
            {
                foreach (var pair in headers)
                {
                    requestMessage.Headers.Add(pair.Key, pair.Value);
                }
            }

            return requestMessage;
        }
    }
}
