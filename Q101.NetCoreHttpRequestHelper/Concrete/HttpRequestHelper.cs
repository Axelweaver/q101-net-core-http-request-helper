using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Q101.NetCoreHttpRequestHelper.Abstract;
using Q101.NetCoreHttpRequestHelper.Converters.Abstract;
using Q101.NetCoreHttpRequestHelper.Enums;

namespace Q101.NetCoreHttpRequestHelper.Concrete
{
    /// <inheritdoc />
    public class HttpRequestHelper : IHttpRequestHelper
    {
        /// <inheritdoc />
        public bool UseCamelCase { get; set; } = false;

        /// <inheritdoc />
        public Dictionary<string, string> DefaultHeaders { get; set; }

        #region privates

        // http client
        private readonly HttpClient _httpClient;

        // http request sender
        private readonly IHttpRequestSender _requestSender;

        /// Authentication header
        private AuthenticationHeaderValue _authenticationHeader;

        #endregion

        #region public properties

        /// <inheritdoc />
        public IHttpRequestSpecificTypeSender Get { get; }

        /// <inheritdoc />
        public IHttpRequestSpecificTypeSender Post { get; }

        /// <inheritdoc />
        public IHttpRequestSpecificTypeSender Put { get; }

        /// <inheritdoc />
        public IHttpRequestSpecificTypeSender Delete { get; }

        /// <inheritdoc />
        public IHttpRequestSpecificTypeSender Patch { get; }

        #endregion

        #region ctor

        /// <summary>
        /// ctor.
        /// </summary>
        public HttpRequestHelper(IJsonConverterAdapter jsonConverter,
                                 IXmlConverter xmlConverter,
                                 HttpClient httpClient)
        {
            _httpClient = httpClient;

            Func<HttpClient> httpClientFunc = ConfigureHttpClient;

            _requestSender = new HttpRequestSender(jsonConverter, xmlConverter, UseCamelCase);

            Get = new HttpRequestSpecificTypeSender(httpClientFunc, _requestSender, HttpMethod.Get);
            Post = new HttpRequestSpecificTypeSender(httpClientFunc, _requestSender, HttpMethod.Post);
            Put = new HttpRequestSpecificTypeSender(httpClientFunc, _requestSender, HttpMethod.Put);
            Delete = new HttpRequestSpecificTypeSender(httpClientFunc, _requestSender, HttpMethod.Delete);
            Patch = new HttpRequestSpecificTypeSender(httpClientFunc, _requestSender, HttpMethod.Patch);
        }

        #endregion

        #region public methods

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
        public async Task<T> SendRequestAsync<T>(ContentTypes contentType,
                                                 HttpMethod method,
                                                 string url,
                                                 object body = null,
                                                 Encoding encoding = null,
                                                 Dictionary<string, string> headers = null)
        {
            return await _requestSender.SendRequestAsync<T>(ConfigureHttpClient,
                                                            contentType,
                                                            method, 
                                                            url, 
                                                            body, 
                                                            encoding, 
                                                            headers);
        }        

        #endregion


        /// <summary>
        /// Http client configure.
        /// </summary>
        private HttpClient ConfigureHttpClient()
        {
            if (_authenticationHeader != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = _authenticationHeader;
            }

            foreach (var header in _httpClient.DefaultRequestHeaders)
            {
                _httpClient.DefaultRequestHeaders.Remove(header.Key);
            }

            if (DefaultHeaders != null)
            {
                foreach (var pair in DefaultHeaders)
                {
                    //_httpClient.DefaultRequestHeaders.Remove(pair.Key);
                    _httpClient.DefaultRequestHeaders.Add(pair.Key, pair.Value);
                }
            }

            return _httpClient;
        }
    }
}
