using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Q101.NetCoreHttpRequestHelper.Abstract;
using Q101.NetCoreHttpRequestHelper.Converters.Abstract;

namespace Q101.NetCoreHttpRequestHelper.Concrete
{
    /// <inheritdoc />
    public class HttpRequestSender : IHttpRequestSender
    {
        /// use camelCase option
        private readonly bool _useCamelCase;

        // json conveter.
        private readonly IJsonConverterAdapter _jsonConverter;

        // http client creator
        private readonly IHttpClientCreator _httpClientCreator;

        /// <summary>
        /// ctor
        /// </summary>
        public HttpRequestSender(IJsonConverterAdapter jsonConverter, 
                                 IHttpClientCreator httpClientCreator,
                                 bool useCamelCase)
        {
            _jsonConverter = jsonConverter;
            _httpClientCreator = httpClientCreator;
            _useCamelCase = useCamelCase;
        }

        /// <summary>
        /// Sent http request with header Content-type = application/json.
        /// </summary>
        /// <typeparam name="T">Returned object type.</typeparam>
        /// <param name="method">Http request method.</param>
        /// <param name="url">Http request url.</param>
        /// <param name="body">Http request body.</param>
        /// <param name="headers">Additional http request headers.</param>
        public async Task<T> SendJsonRequestAsync<T>(HttpMethod method,
                                                     string url,
                                                     object body = null,
                                                     Dictionary<string, string> headers = null)
        {
            using (var httpClient = _httpClientCreator.Create())
            {
                var requestMessage = new HttpRequestMessage(method, url);

                if (body != null)
                {
                    var json = _useCamelCase
                        ? _jsonConverter.SerializeCamelCase(body)
                        : _jsonConverter.Serialize(body);

                    var requestBody = new StringContent(json, Encoding.UTF8, "application/json");

                    requestMessage.Content = requestBody;
                }

                if (headers != null)
                {
                    foreach (var pair in headers)
                    {
                        requestMessage.Headers.Add(pair.Key, pair.Value);
                    }
                }

                var response = await httpClient.SendAsync(requestMessage);
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"{response.ReasonPhrase} ({response.StatusCode})\n{content}");
                }

                var result = DeserializeContentString<T>(content);

                return result;
            }
        }

        /// <summary>
        /// Deserialization request body string to object
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="content">Body content string.</param>
        private T DeserializeContentString<T>(string content)
        {
            T result = default;

            if (!string.IsNullOrWhiteSpace(content))
            {
                result = _jsonConverter.Deserialize<T>(content);
            }

            return result;
        }
    }
}
