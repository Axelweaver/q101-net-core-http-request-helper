using System.Collections.Generic;
using System.Threading.Tasks;

namespace Q101.NetCoreHttpRequestHelper.Abstract
{
    /// <summary>
    /// Specific type request sender
    /// </summary>
    public interface IHttpRequestSpecificTypeSender
    {
        /// <summary>
        /// Send http GET request with application/json and get response object.
        /// </summary>
        /// <typeparam name="T">Response object type.</typeparam>
        /// <param name="url">Request url.</param>
        /// <param name="body">Request body object</param>
        /// <param name="headers">Additional request headers.</param>
        Task<T> SendAsync<T>(string url, object body = null, Dictionary<string, string> headers = null);

        /// <summary>
        /// Send http GET request.
        /// </summary>
        /// <param name="url">Request url.</param>
        /// <param name="body">Request body object</param>
        /// <param name="headers">Additional request headers.</param>
        Task SendAsync(string url, object body = null, Dictionary<string, string> headers = null);
    }
}
