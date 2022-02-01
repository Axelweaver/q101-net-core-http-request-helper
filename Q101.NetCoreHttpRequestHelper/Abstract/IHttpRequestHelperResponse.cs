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
        /// HttpResponseMessage returned by client.
        /// </summary>
        HttpResponseMessage Response { get; }

        /// <summary>
        /// Deserialize as JSON.
        /// </summary>
        /// <typeparam name="TResult">Result type.</typeparam>
        /// <returns>Deserialized object of TResult.</returns>
        Task<TResult> JsonAsync<TResult>();

        /// <summary>
        /// Deserialize as XML.
        /// </summary>
        /// <typeparam name="TResult">Result type.</typeparam>
        /// <returns>Deserialized object of TResult.</returns>
        Task<TResult> XmlAsync<TResult>();

        /// <summary>
        /// Get response as Stream.
        /// </summary>
        Task<Stream> StreamAsync();

        /// <summary>
        /// Get response as string.
        /// </summary>
        Task<string> StringAsync();

        /// <summary>
        /// Get response as byte array.
        /// </summary>
        Task<byte[]> BytesAsync();
    }
}
