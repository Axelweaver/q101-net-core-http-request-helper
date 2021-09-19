using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Q101.NetCoreHttpRequestHelper.Abstract
{
    /// <summary>
    /// Specific type request sender
    /// </summary>
    public interface IHttpRequestSpecificTypeSender
    {
        /// <summary>
        /// Send http GET request with text/xml and get response object.
        /// </summary>
        /// <typeparam name="T">Response object type.</typeparam>
        /// <param name="url">Request url.</param>
        /// <param name="body">Request body object.</param>
        /// <param name="encoding">Encoding.</param>
        /// <param name="headers">Additional request headers.</param>
        Task<T> SendJsonAsync<T>(string url,
                                 object body = null,
                                 Encoding encoding = null,
                                 Dictionary<string, string> headers = null);

        /// <summary>
        /// Send http GET request.
        /// </summary>
        /// <param name="url">Request url.</param>
        /// <param name="body">Request body object</param>
        /// <param name="encoding">Encoding.</param>
        /// <param name="headers">Additional request headers.</param>
        Task SendJsonAsync(string url,
                           object body = null,
                           Encoding encoding = null,
                           Dictionary<string, string> headers = null);

        /// <summary>
        /// Send http GET request with text/xml and get response object.
        /// </summary>
        /// <typeparam name="T">Response object type.</typeparam>
        /// <param name="url">Request url.</param>
        /// <param name="body">Request body object</param>
        /// <param name="encoding">Encoding.</param>
        /// <param name="headers">Additional request headers.</param>
        Task<T> SendXmlAsync<T>(string url,
                                object body = null,
                                Encoding encoding = null,
                                Dictionary<string, string> headers = null);

        /// <summary>
        /// Send http GET request.
        /// </summary>
        /// <param name="url">Request url.</param>
        /// <param name="body">Request body object</param>
        /// <param name="encoding">Encoding.</param>
        /// <param name="headers">Additional request headers.</param>
        Task SendXmlAsync(string url,
                          object body = null,
                          Encoding encoding = null,
                          Dictionary<string, string> headers = null);
    }
}
