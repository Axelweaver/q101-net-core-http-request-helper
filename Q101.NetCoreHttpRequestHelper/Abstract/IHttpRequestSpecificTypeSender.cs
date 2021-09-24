using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Q101.NetCoreHttpRequestHelper.Enums;

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
        /// <param name="contentType">Request body type.</param>
        /// <param name="url">Request url.</param>
        /// <param name="body">Request body object.</param>
        /// <param name="encoding">Encoding.</param>
        /// <param name="headers">Additional request headers.</param>
        Task<T> SendAsync<T>(ContentTypes contentType, 
                             string url,
                             object body = null,
                             Encoding encoding = null,
                             Dictionary<string, string> headers = null);

        /// <summary>
        /// Send http GET request.
        /// </summary>
        /// <param name="contentType">Request body type.</param>
        /// <param name="url">Request url.</param>
        /// <param name="body">Request body object</param>
        /// <param name="encoding">Encoding.</param>
        /// <param name="headers">Additional request headers.</param>
        Task SendAsync(ContentTypes contentType, 
                       string url,
                       object body = null,
                       Encoding encoding = null,
                       Dictionary<string, string> headers = null);

        /// <summary>
        /// Send http request with stream response.
        /// </summary>
        /// <param name="contentType">Request body type.</param>
        /// <param name="url">Request url.</param>
        /// <param name="body">Request body object</param>
        /// <param name="encoding">Encoding.</param>
        /// <param name="headers">Additional request headers.</param>
        /// <returns></returns>
        Task<Stream> SendWithStreamResponse(ContentTypes contentType,
                                            string url,
                                            object body = null,
                                            Encoding encoding = null,
                                            Dictionary<string, string> headers = null);


        /// <summary>
        /// Send http request with bytes array response.
        /// </summary>
        /// <param name="contentType">Request body type.</param>
        /// <param name="url">Request url.</param>
        /// <param name="body">Request body object</param>
        /// <param name="encoding">Encoding.</param>
        /// <param name="headers">Additional request headers.</param>
        /// <returns></returns>
        Task<byte[]> SendWithBytesResponse(ContentTypes contentType,
                                           string url,
                                           object body = null,
                                           Encoding encoding = null,
                                           Dictionary<string, string> headers = null);
    }
}
