using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Q101.NetCoreHttpRequestHelper
{
    /// <summary>
    /// HttpRequestHelper formatted content sender.
    /// </summary>
    public interface IHttpContentSender<TContent>
    {
        /// <summary>
        /// Execute GET request.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="content">Request content.</param>
        /// <param name="headers">Request additional headers.</param>
        /// <param name="encoding">Request encoding (default UTF-8).</param>
        /// <param name="ensureSuccess">Call HttpRequestMessage EnsureSuccesStatusCode after request.</param>
        /// <returns>HttpClient response wrapper.</returns>
        Task<IHttpRequestHelperResponse> GetAsync(
            string url,
            TContent content = default,
            Dictionary<string, string> headers = null,
            Encoding encoding = null,
            bool ensureSuccess = true);

        /// <summary>
        /// Execute POST request.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="content">Request content.</param>
        /// <param name="headers">Request additional headers.</param>
        /// <param name="encoding">Request encoding (default UTF-8).</param>
        /// <param name="ensureSuccess">Call HttpRequestMessage EnsureSuccesStatusCode after request.</param>
        /// <returns>HttpClient response wrapper.</returns>
        Task<IHttpRequestHelperResponse> PostAsync(
            string url,
            TContent content = default,
            Dictionary<string, string> headers = null,
            Encoding encoding = null,
            bool ensureSuccess = true);

        /// <summary>
        /// Execute PUT request.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="content">Request content.</param>
        /// <param name="headers">Request additional headers.</param>
        /// <param name="encoding">Request encoding (default UTF-8).</param>
        /// <param name="ensureSuccess">Call HttpRequestMessage EnsureSuccesStatusCode after request.</param>
        /// <returns>HttpClient response wrapper.</returns>
        Task<IHttpRequestHelperResponse> PutAsync(
            string url,
            TContent content = default,
            Dictionary<string, string> headers = null,
            Encoding encoding = null,
            bool ensureSuccess = true);

        /// <summary>
        /// Execute DELETE request.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="content">Request content.</param>
        /// <param name="headers">Request additional headers.</param>
        /// <param name="encoding">Request encoding (default UTF-8).</param>
        /// <param name="ensureSuccess">Call HttpRequestMessage EnsureSuccesStatusCode after request.</param>
        /// <returns>HttpClient response wrapper.</returns>
        Task<IHttpRequestHelperResponse> DeleteAsync(
            string url,
            TContent content = default,
            Dictionary<string, string> headers = null,
            Encoding encoding = null,
            bool ensureSuccess = true);

        /// <summary>
        /// Execute PATCH request.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="content">Request content.</param>
        /// <param name="headers">Request additional headers.</param>
        /// <param name="encoding">Request encoding (default UTF-8).</param>
        /// <param name="ensureSuccess">Call HttpRequestMessage EnsureSuccesStatusCode after request.</param>
        /// <returns>HttpClient response wrapper.</returns>
        Task<IHttpRequestHelperResponse> PatchAsync(
            string url,
            TContent content = default,
            Dictionary<string, string> headers = null,
            Encoding encoding = null,
            bool ensureSuccess = true);

        /// <summary>
        /// Send http request.
        /// </summary>
        /// <param name="method">Request method.</param>
        /// <param name="url">Request URL.</param>
        /// <param name="content">Request content.</param>
        /// <param name="headers">Request additional headers.</param>
        /// <param name="encoding">Request encoding (default UTF-8).</param>
        /// <param name="ensureSuccess">Call HttpRequestMessage EnsureSuccesStatusCode after request.</param>
        /// <returns>HttpClient response wrapper.</returns>
        Task<IHttpRequestHelperResponse> SendAsync(
            HttpMethod method,
            string url,
            TContent content = default,
            Dictionary<string, string> headers = null,
            Encoding encoding = null,
            bool ensureSuccess = true);
    }
}
