using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Q101.NetCoreHttpRequestHelper
{
    /// <summary>
    /// Http response message returned by client.
    /// </summary>
    public interface IHttpRequestHelperResponse
    {
        /// <summary>
        /// Get HttpResponseMessage returned by client.
        /// </summary>
        Task<HttpResponseMessage> Response();

        /// <summary>
        /// Deserialize as JSON.
        /// </summary>
        /// <typeparam name="TResult">Result type.</typeparam>
        /// <returns>Deserialized object of TResult.</returns>
        Task<TResult> Json<TResult>();

        /// <summary>
        /// Deserialize as XML.
        /// </summary>
        /// <typeparam name="TResult">Result type.</typeparam>
        /// <returns>Deserialized object of TResult.</returns>
        Task<TResult> Xml<TResult>();

        /// <summary>
        /// Get response as Stream.
        /// </summary>
        Task<Stream> Stream();

        /// <summary>
        /// Get response as byte array.
        /// </summary>
        Task<byte[]> Bytes();
    }
}
