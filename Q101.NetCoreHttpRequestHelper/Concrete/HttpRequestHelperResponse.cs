using Q101.NetCoreHttpRequestHelper.Converters;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Q101.NetCoreHttpRequestHelper
{
    /// <summary>
    /// HttpRequestHelper response wrapper.
    /// </summary>
    internal class HttpRequestHelperResponse : IHttpRequestHelperResponse
    {
        #region private props

        /// <summary>
        /// Reponse message returned by client.
        /// </summary>
        private readonly HttpResponseMessage _httpResponse;

        /// <summary>
        /// Use camel case on deserialization.
        /// </summary>
        private readonly bool _useCamelCase;

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
        /// HttpRequestHelper response wrapper.
        /// </summary>
        public HttpRequestHelperResponse(
            HttpResponseMessage httpResponse,
            bool useCamelCase,
            IJsonConverter jsonConverter,
            IXmlConverter xmlConverter)
        {
            _httpResponse = httpResponse;
            _useCamelCase = useCamelCase;
            _jsonConverter = jsonConverter;
            _xmlConverter = xmlConverter;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> Response()
        {
            return _httpResponse;
        }

        /// <inheritdoc/>
        public async Task<TResult> Json<TResult>()
        {
            var content = await _httpResponse.Content.ReadAsStringAsync();

            var result = _useCamelCase
                ? _jsonConverter.DeserializeCamelCase<TResult>(content)
                : _jsonConverter.Deserialize<TResult>(content);

            return result;
        }

        /// <inheritdoc/>
        public async Task<TResult> Xml<TResult>()
        {
            var content = await _httpResponse.Content.ReadAsStringAsync();

            var result = _xmlConverter.Deserialize<TResult>(content);

            return result;
        }

        /// <inheritdoc/>
        public async Task<Stream> Stream()
        {
            var stream = await _httpResponse.Content.ReadAsStreamAsync();

            return stream;
        }

        /// <inheritdoc/>
        public async Task<byte[]> Bytes()
        {
            var bytes = await _httpResponse.Content.ReadAsByteArrayAsync();

            return bytes;
        }
    }
}
