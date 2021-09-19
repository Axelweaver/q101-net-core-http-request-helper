namespace Q101.NetCoreHttpRequestHelper.Converters.Abstract
{
    /// <summary>
    /// JSON converter adapter
    /// </summary>
    public interface IJsonConverterAdapter
    {
        /// <summary>
        /// Serialize object to JSON
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="obj">Object</param>
        string Serialize<T>(T obj);

        /// <summary>
        /// Serialize object to JSON camelCase.
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="obj">Object</param>
        string SerializeCamelCase<T>(T obj);

        /// <summary>
        /// Deserialize JSON to object
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="json">JSON value</param>
        T Deserialize<T>(string json);
    }
}
