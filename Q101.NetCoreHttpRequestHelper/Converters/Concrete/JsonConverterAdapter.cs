using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Q101.NetCoreHttpRequestHelper.Converters.Abstract;

namespace Q101.NetCoreHttpRequestHelper.Converters.Concrete
{
    /// <inheritdoc />
    public class JsonConverterAdapter : IJsonConverterAdapter
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
    }
}
