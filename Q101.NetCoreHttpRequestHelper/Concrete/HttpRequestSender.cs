using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Q101.NetCoreHttpRequestHelper.Abstract;
using Q101.NetCoreHttpRequestHelper.Converters.Abstract;
using Q101.NetCoreHttpRequestHelper.Enums;

namespace Q101.NetCoreHttpRequestHelper.Concrete
{
    /// <inheritdoc />
    public class HttpRequestSender : IHttpRequestSender
    {
        #region privates

        /// use camelCase option.
        private readonly bool _useCamelCase;

        // json conveter.
        private readonly IJsonConverterAdapter _jsonConverter;

        // xml converter.
        private readonly IXmlConverter _xmlConverter;

        #endregion

        #region ctor

        /// <summary>
        /// ctor
        /// </summary>
        public HttpRequestSender(IJsonConverterAdapter jsonConverter, 
                                 IXmlConverter xmlConverter,
                                 bool useCamelCase)
        {
            _jsonConverter = jsonConverter;
            _xmlConverter = xmlConverter;
            _useCamelCase = useCamelCase;
        }

        #endregion

        #region public methods

        /// <inheritdoc />
        public async Task<T> SendRequestAsync<T>(Func<HttpClient> httpClientFunc,
                                                 ContentTypes contentType,
                                                 HttpMethod method,
                                                 string url,
                                                 object body = null,
                                                 Encoding encoding= null,
                                                 IDictionary<string, string> headers = null)
        {
            var httpClient = httpClientFunc();
            var requestMessage = GetRequestContent(contentType, 
                                                             url, 
                                                             method, 
                                                             body, 
                                                             encoding, 
                                                             headers);

            var result = await GetResponseObject<T>(httpClient, requestMessage, contentType);

            return result;
        }

        /// <inheritdoc />
        public async Task<Stream> SendRequestWithStreamResponseAsync(Func<HttpClient> httpClientFunc,
                                                                     ContentTypes contentType,
                                                                     string url,
                                                                     HttpMethod method,
                                                                     object body = null,
                                                                     Encoding encoding = null,
                                                                     IDictionary<string, string> headers = null)
        {
            var httpClient = httpClientFunc();

            var requestMessage = GetRequestContent(contentType,
                                                   url,
                                                   method,
                                                   body,
                                                   encoding,
                                                   headers);

            var response = await httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStreamAsync();

            return result;
        }

        /// <inheritdoc />
        public async Task<byte[]> SendRequestWithBytesResponseAsync(Func<HttpClient> httpClientFunc,
                                                                    ContentTypes contentType,
                                                                    string url,
                                                                    HttpMethod method,
                                                                    object body = null,
                                                                    Encoding encoding = null,
                                                                    IDictionary<string, string> headers = null)
        {
            var httpClient = httpClientFunc();

            var requestMessage = GetRequestContent(contentType,
                                                   url,
                                                   method,
                                                   body,
                                                   encoding,
                                                   headers);

            var response = await httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsByteArrayAsync();

            return result;
        }

        #endregion

        #region private methods

        /// <summary>
        /// Get http request message with string body content.
        /// </summary>
        /// <param name="contentType">Request body content type.</param>
        /// <param name="url">Request url.</param>
        /// <param name="method">Request method.</param>
        /// <param name="body">Request body.</param>
        /// <param name="encoding">Request body content encoding.</param>
        /// <param name="headers">Additional request headers.</param>
        /// <returns></returns>
        private HttpRequestMessage GetRequestContent(ContentTypes contentType, 
                                                     string url, 
                                                     HttpMethod method, 
                                                     object body = null,
                                                     Encoding encoding = null,
                                                     IDictionary<string, string> headers = null)
        {
            var requestMessage = new HttpRequestMessage(method, url);

            AddRequestHeaders(requestMessage, headers);

            if (body == null)
            {
                return requestMessage;
            }

            if (contentType == ContentTypes.Stream)
            {
                requestMessage.Content = new StreamContent((Stream)body);

                return requestMessage;
            }

            if (contentType == ContentTypes.Bytes)
            {
                requestMessage.Content = new ByteArrayContent((byte[])body);

                return requestMessage;
            }

            var bodyContent = 
                contentType == ContentTypes.Xml
                    ? _xmlConverter.Serialize(body)
                    : _useCamelCase
                        ? _jsonConverter.SerializeCamelCase(body)
                        : _jsonConverter.Serialize(body);

            var requestContentType =
                contentType == ContentTypes.Xml
                    ? "text/xml"
                    : "application/json";

            var requestBody = new StringContent(bodyContent, encoding, requestContentType);

            requestMessage.Content = requestBody;

            return requestMessage;
        }

        /// <summary>
        /// Add headers to http request.
        /// </summary>
        /// <param name="requestMessage">Http request message.</param>
        /// <param name="headers">headers collection</param>
        private void AddRequestHeaders(HttpRequestMessage requestMessage, 
                                       IDictionary<string, string> headers)
        {
            if (headers == null || !headers.Any())
            {
                return;
            }

            foreach (var pair in headers)
            {
                if (requestMessage.Headers.Contains(pair.Key))
                {
                    requestMessage.Headers.Remove(pair.Key);
                }

                requestMessage.Headers.Add(pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// Get deserialized object from http request.
        /// </summary>
        /// <typeparam name="T">Response object type.</typeparam>
        /// <param name="httpClient">Http client.</param>
        /// <param name="request">Http request message.</param>
        /// <param name="contentType">Http response content type</param>
        /// <returns></returns>
        private async Task<T> GetResponseObject<T>(HttpClient httpClient, 
                                                  HttpRequestMessage request, 
                                                  ContentTypes contentType)
        {
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var result = DeserializeContentString<T>(contentType, content);

            return result;
        }

        /// <summary>
        /// Deserialization request body string to object
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="contentType">Request body content type.</param>
        /// <param name="content">Body content string.</param>
        private T DeserializeContentString<T>(ContentTypes contentType,
                                              string content)
        {
            T result = default;

            if (!string.IsNullOrWhiteSpace(content))
            {
                result = contentType == ContentTypes.Xml
                ? _xmlConverter.Deserialize<T>(content)
                : _jsonConverter.Deserialize<T>(content);
            }

            return result;
        }

        #endregion

    }
}
