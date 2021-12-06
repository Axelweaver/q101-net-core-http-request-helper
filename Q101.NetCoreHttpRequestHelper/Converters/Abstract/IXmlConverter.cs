using System.Collections.Generic;
using System.Text;

namespace Q101.NetCoreHttpRequestHelper.Converters
{
    /// <summary>
    /// Xml serialization converter.
    /// </summary>
    public interface IXmlConverter
    {
        /// <summary>
        /// Serialize object to xml string.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="data">Object.</param>
        /// <param name="encoding">Serialization encoding (UTF-8 by default).</param>
        /// <returns>Serialized xml string.</returns>
        string Serialize<T>(T data, Encoding encoding = null);

        /// <summary>
        /// Deserialize xml string to type T.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="xml">Xml string.</param>
        /// <returns>Deserialized object.</returns>
        T Deserialize<T>(string xml);

        /// <summary>
        /// Deserialize string to object T type with xml node errors logging.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="xml">Xml string.</param>
        /// <param name="errors">Errors list.</param>
        /// <returns>Deserialized object.</returns>
        T DeserializeWithLogging<T>(string xml, out List<string> errors);
    }
}
