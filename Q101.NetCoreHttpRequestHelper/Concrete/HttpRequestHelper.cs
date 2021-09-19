using System.Collections.Generic;
using System.Net.Http;
using Q101.NetCoreHttpRequestHelper.Abstract;
using Q101.NetCoreHttpRequestHelper.Converters.Abstract;

namespace Q101.NetCoreHttpRequestHelper.Concrete
{
    /// <inheritdoc />
    public class HttpRequestHelper : IHttpRequestHelper
    {
        /// <inheritdoc />
        public bool DisableSslCheck { get; set; } = false;

        /// <inheritdoc />
        public bool UseCamelCase { get; set; } = false;

        /// <inheritdoc />
        public Dictionary<string, string> DefaultHeaders
        {
            get => HttpClientCreator.DefaultHeaders;

            set => HttpClientCreator.DefaultHeaders = value;
        }

        /// <summary>
        /// Http client creator
        /// </summary>
        private readonly IHttpClientCreator _httpClientCreator;

        /// <inheritdoc />
        public IHttpRequestSpecificTypeSender Get { get; }

        /// <inheritdoc />
        public IHttpRequestSpecificTypeSender Post { get; }

        /// <inheritdoc />
        public IHttpRequestSpecificTypeSender Put { get; }

        /// <inheritdoc />
        public IHttpRequestSpecificTypeSender Delete { get; }

        /// <summary>
        /// ctor.
        /// </summary>
        public HttpRequestHelper(IJsonConverterAdapter jsonConverter)
        {
            _httpClientCreator = new HttpClientCreator(DisableSslCheck);
            
            var requestSender = new HttpRequestSender(jsonConverter, _httpClientCreator, UseCamelCase);

            Get = new HttpRequestSpecificTypeSender(requestSender, HttpMethod.Get);
            Post = new HttpRequestSpecificTypeSender(requestSender, HttpMethod.Post);
            Put = new HttpRequestSpecificTypeSender(requestSender, HttpMethod.Put);
            Delete = new HttpRequestSpecificTypeSender(requestSender, HttpMethod.Delete);
        }

        /// <inheritdoc />
        public void SetAuthorizationHeader(string login, string password)
        {
            _httpClientCreator.SetAuthorizationHeader(login, password);
        }

        /// <inheritdoc />
        public void SetAuthorizationHeader(string token)
        {
            _httpClientCreator.SetAuthorizationHeader(token);
        }
    }
}
