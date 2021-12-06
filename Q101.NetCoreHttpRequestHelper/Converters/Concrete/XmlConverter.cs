using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Q101.NetCoreHttpRequestHelper.Converters
{
    /// <summary>
    /// Xml serialization converter.
    /// </summary>
    public class XmlConverter : IXmlConverter
    {
        /// <inheritdoc />
        public string Serialize<T>(T data, Encoding encoding = null)
        {
            encoding ??= Encoding.Default;

            var text = "";

            using (var ms = new MemoryStream())
            using (var writer = new StreamWriter(ms, encoding))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));

                // Get namespace for apply root xml from body. Redefine defaults
                var namespaces = (XmlSerializerNamespaces)typeof(T)
                    .GetProperties()
                    ?.Where(p => typeof(XmlSerializerNamespaces).IsAssignableFrom(p.PropertyType))
                    ?.FirstOrDefault()
                    ?.GetValue(data);

                xmlSerializer.Serialize(writer, data, namespaces);

                text = encoding.GetString(ms.ToArray());
            }

            return text;
        }

        /// <inheritdoc />
        public T Deserialize<T>(string xml)
        {
            T result = default;

            var serializer = new XmlSerializer(typeof(T));

            if (!string.IsNullOrWhiteSpace(xml))
            {
                result = Deserialize<T>(serializer, xml);
            }

            return result;
        }

        /// <inheritdoc />
        public T DeserializeWithLogging<T>(string xml, out List<string> errors)
        {
            errors = new List<string>();

            var serializer = new XmlSerializer(typeof(T));
            var messages = new List<string>();

            void addMessage(string msg)
            {
                if (!messages.Contains(msg))
                {
                    messages.Add(msg);
                }
            };

            // log xml errors.
            serializer.UnknownAttribute += (o, e) =>
            {
                string msg = $"Unknown Attribute {e.Attr.Name}, line: {e.LineNumber}, position: {e.LinePosition}";
                addMessage(msg);
            };

            serializer.UnknownElement += (o, e) =>
            {
                string msg = $"Unknown Element {e.Element.Name}, line: {e.LineNumber}, position: {e.LinePosition}";
                addMessage(msg);
            };

            serializer.UnknownNode += (o, e) =>
            {
                string msg = $"Unknown Node {e.Name}, line: {e.LineNumber}, position: {e.LinePosition}";
                addMessage(msg);
            };

            // deserialize.
            T result = default;

            if (!string.IsNullOrWhiteSpace(xml))
            {
                try
                {
                    result = Deserialize<T>(serializer, xml);
                    errors = messages;
                }
                catch
                {
                    errors = messages;
                    throw;
                }
            }

            return result;
        }

        /// <inheritdoc />
        private T Deserialize<T>(XmlSerializer serializer, string xml)
        {
            try
            {
                using (var textReader = new StringReader(xml))
                {
                    T result = (T)serializer.Deserialize(textReader);

                    return result;
                }
            }
            catch (Exception exc)
            {
                var msg = GetInnerExceptionMessages(exc, "Xml deserialize errors:");

                throw new Exception(msg);
            }
        }

        /// <summary>
        /// Get combined error string for exception and inner exceptions.
        /// </summary>
        private string GetInnerExceptionMessages(Exception exc, string messageStart = "")
        {
            string result = "";

            messageStart = $"{messageStart}\n{exc.Message}";

            if (exc.InnerException != null)
            {
                result = GetInnerExceptionMessages(exc.InnerException, messageStart);
            }

            return result;
        }
    }
}
