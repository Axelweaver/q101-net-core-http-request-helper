using System.Collections.Generic;
using System.Text;

namespace Q101.NetCoreHttpRequestHelper
{
    /// <summary>
    /// Helper request options.
    /// </summary>
    public class HttpRequestHelperOptions
    {
        /// <summary>
        /// Headers added to requests by default.
        /// </summary>
        public Dictionary<string, string> DefaultHeaders { get; set; }
            = new Dictionary<string, string>();

        /// <summary>
        /// Requests encoding by default (UTF-8 if unset).
        /// </summary>
        public Encoding DefaultEncoding { get; set; }
            = Encoding.UTF8;

        /// <summary>
        /// Use camel case on serialization.
        /// </summary>
        public bool UseCamelCase { get; set; }
    }
}
