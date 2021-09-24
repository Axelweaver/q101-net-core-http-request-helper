using System;
using Q101.NetCoreHttpRequestHelper.Abstract;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Q101.NetCoreHttpRequestHelper.Enums;

namespace Q101.NetCoreHttpRequestHelper.Concrete
{
    /// <inheritdoc />
    public class HttpRequestSpecificTypeSender : IHttpRequestSpecificTypeSender
    {
        #region privates

        // http request sender
        private readonly IHttpRequestSender _requestSender;

        // http request method
        private readonly HttpMethod _httpMethod;

        //  http client func
        private readonly Func<HttpClient> _httpClientFunc;

        #endregion

        #region ctor

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="httpClientFunc">Http client func.</param>
        /// <param name="requestSender">Http request sender.</param>
        /// <param name="httpMethod">Http request method.</param>
        public HttpRequestSpecificTypeSender(Func<HttpClient> httpClientFunc, 
                                             IHttpRequestSender requestSender, 
                                             HttpMethod httpMethod)
        {
            _httpClientFunc = httpClientFunc;
            _requestSender = requestSender;
            _httpMethod = httpMethod;
        }

        #endregion


        /// <inheritdoc />
        public async Task<T> SendAsync<T>(ContentTypes contentType,
                                          string url, 
                                          object body = null, 
                                          Encoding encoding = null, 
                                          Dictionary<string, string> headers = null)
        {
            return await _requestSender.SendRequestAsync<T>(_httpClientFunc, 
                                                            contentType,
                                                            _httpMethod,
                                                            url,
                                                            body,
                                                            encoding,
                                                            headers);
        }

        /// <inheritdoc />
        public async Task SendAsync(ContentTypes contentType,
                                    string url,
                                    object body = null,
                                    Encoding encoding = null,
                                    Dictionary<string, string> headers = null)
        {
            await _requestSender.SendRequestAsync<object>(_httpClientFunc,
                                                          contentType,
                                                          _httpMethod,
                                                          url,
                                                          body,
                                                          encoding,
                                                          headers);
        }

        public async Task<Stream> SendWithStreamResponse(ContentTypes contentType, 
                                                         string url,
                                                         object body = null,
                                                         Encoding encoding = null,
                                                         Dictionary<string, string> headers = null)
        {
            return await _requestSender.SendRequestWithStreamResponseAsync(_httpClientFunc,
                                                                           contentType,
                                                                           url,
                                                                           _httpMethod,
                                                                           body,
                                                                           encoding,
                                                                           headers);
        }

        public async Task<byte[]> SendWithBytesResponse(ContentTypes contentType, 
                                                        string url,
                                                        object body = null,
                                                        Encoding encoding = null,
                                                        Dictionary<string, string> headers = null)
        {
           return await _requestSender.SendRequestWithBytesResponseAsync(_httpClientFunc,
                                                                         contentType,
                                                                         url,
                                                                         _httpMethod,
                                                                         body,
                                                                         encoding,
                                                                         headers);

        }
    }
}
