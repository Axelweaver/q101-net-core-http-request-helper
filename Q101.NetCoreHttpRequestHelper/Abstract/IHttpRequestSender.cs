using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Q101.NetCoreHttpRequestHelper.Abstract
{
    /// <summary>
    /// Http request sender
    /// </summary>
    public interface IHttpRequestSender
    {
        /// <summary>
        /// Sent http request with header Content-type = application/json.
        /// </summary>
        /// <typeparam name="T">Type of returned response object.</typeparam>
        /// <param name="method">Http method.</param>
        /// <param name="url">Http request url.</param>
        /// <param name="body">Http request body.</param>
        /// <param name="headers">Http additional request headers.</param>
        /// <returns></returns>
        Task<T> SendJsonRequestAsync<T>(HttpMethod method,
                                        string url,
                                        object body = null,
                                        Dictionary<string, string> headers = null);
    }
}
