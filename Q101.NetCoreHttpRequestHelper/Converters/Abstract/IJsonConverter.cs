namespace Q101.NetCoreHttpRequestHelper.Converters
{
    /// <summary>
    /// JSON converter.
    /// </summary>
    public interface IJsonConverter
    {
        /// <summary>
        /// Serialize object to JSON string.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="obj">Object.</param>
        /// <returns>Serialized JSON string.</returns>
        string Serialize<T>(T obj);

        /// <summary>
        /// Serialize object to JSON camelCase string.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="obj">Object.</param>
        /// <returns>Serialized JSON string.</returns>
        string SerializeCamelCase<T>(T obj);

        /// <summary>
        /// Deserialize JSON string to type T.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="json">JSON string.</param>
        /// <returns>Deserialized object.</returns>
        T Deserialize<T>(string json);

        /// <summary>
        /// Deserialize JSON camelCase string to type T.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="json">JSON string.</param>
        /// <returns>Deserialized object.</returns>
        T DeserializeCamelCase<T>(string json);
    }
}
