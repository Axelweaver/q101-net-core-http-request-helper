using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Q101.NetCoreHttpRequestHelper.Enums;

namespace Q101.NetCoreHttpRequestHelper.Abstract
{
    /// <summary>
    /// Http request sender
    /// </summary>
    public interface IHttpRequestSender
    {
        /// <summary>
        /// Sent http request .
        /// </summary>
        /// <typeparam name="T">Returned object type.</typeparam>
        /// <param name="httpClientFunc">Get http client func.</param>
        /// <param name="contentType">Http request body content type.</param>
        /// <param name="method">Http request method.</param>
        /// <param name="url">Http request url.</param>
        /// <param name="body">Http request body.</param>
        /// <param name="encoding"></param>
        /// <param name="headers">Additional http request headers.</param>
        /// <returns></returns>
        Task<T> SendRequestAsync<T>(Func<HttpClient> httpClientFunc,
                                    ContentTypes contentType,
                                    HttpMethod method,
                                    string url,
                                    object body = null,
                                    Encoding encoding = null,
                                    Dictionary<string, string> headers = null);
    }
}
