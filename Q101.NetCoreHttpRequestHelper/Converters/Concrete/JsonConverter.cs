using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Q101.NetCoreHttpRequestHelper.Converters
{
    /// <summary>
    /// JSON converter.
    /// </summary>
    public class JsonConverter : IJsonConverter
    {
        /// <inheritdoc />
        public string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <inheritdoc />
        public string SerializeCamelCase<T>(T obj)
        {
            var options = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(obj, options);
        }

        /// <inheritdoc />
        public T Deserialize<T>(string json)
        {
            T result = JsonConvert.DeserializeObject<T>(json);

            return result;
        }

        /// <inheritdoc />
        public T DeserializeCamelCase<T>(string json)
        {
            T result = JsonConvert.DeserializeObject<T>(json);

            return result;
        }
    }
}
