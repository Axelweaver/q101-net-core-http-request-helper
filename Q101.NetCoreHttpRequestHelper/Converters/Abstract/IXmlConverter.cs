using System.Collections.Generic;
using System.Text;

namespace Q101.NetCoreHttpRequestHelper.Converters.Abstract
{
    /// <summary>
    /// Converter for serialize/deserialize xml
    /// </summary>
    public interface IXmlConverter
    {
        /// <summary>
        /// Serialize object to xml string
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="data">Object.</param>
        /// <param name="encoding">Encoding,default  Encoding.Default</param>
        /// <returns>Строка xml.</returns>
        string Serialize<T>(T data, Encoding encoding = null);

        /// <summary>
        /// Deserialize string to object T type.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="xml">Xml string.</param>
        T Deserialize<T>(string xml);

        /// <summary>
        /// Deserialize string to object T type with error logs.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="xml">Xml string.</param>
        /// <param name="errors">Errors list.</param>
        T DeserializeWithLogging<T>(string xml, out List<string> errors);
    }
}
