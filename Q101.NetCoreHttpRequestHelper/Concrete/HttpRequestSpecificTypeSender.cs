using Q101.NetCoreHttpRequestHelper.Abstract;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Q101.NetCoreHttpRequestHelper.Concrete
{
    /// <inheritdoc />
    public class HttpRequestSpecificTypeSender : IHttpRequestSpecificTypeSender
    {
        // http request sender
        private readonly IHttpRequestSender _requestSender;

        // http request method
        private readonly HttpMethod _httpMethod;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="requestSender">Http request sender.</param>
        /// <param name="httpMethod">Http request method.</param>
        public HttpRequestSpecificTypeSender(IHttpRequestSender requestSender, HttpMethod httpMethod)
        {
            _requestSender = requestSender;
            _httpMethod = httpMethod;
        }

        /// <inheritdoc />
        public async Task<T> SendAsync<T>(string url, object body = null, Dictionary<string, string> headers = null)
        {
            return await _requestSender.SendJsonRequestAsync<T>(_httpMethod, url, body, headers);
        }

        /// <inheritdoc />
        public async Task SendAsync(string url, object body = null, Dictionary<string, string> headers = null)
        {
            await _requestSender.SendJsonRequestAsync<object>(_httpMethod, url, body, headers);
        }
    }
}
