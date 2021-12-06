using Microsoft.Extensions.DependencyInjection;
using Q101.NetCoreHttpRequestHelper.Converters;
using System.Net.Http;

namespace Q101.NetCoreHttpRequestHelper
{
    /// <summary>
    /// Service collection extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add IHttpRequestHelper dependencies to service collection.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="disableSslCheck">Disable SSL check for helper requests.</param>
        public static IServiceCollection AddHttpRequestHelper(
            this IServiceCollection services,
            bool disableSslCheck = false)
        {
            services.AddSingleton<IJsonConverter, JsonConverter>();
            services.AddSingleton<IXmlConverter, XmlConverter>();

            if (!disableSslCheck)
            {
                services.AddHttpClient<IHttpRequestHelper, HttpRequestHelper>();

                return services;
            }

            services.AddHttpClient<IHttpRequestHelper, HttpRequestHelper>()
                .ConfigurePrimaryHttpMessageHandler(() => {
                    // Disable SSL check.
                    return new HttpClientHandler
                    {
                        ClientCertificateOptions = ClientCertificateOption.Manual,
                        ServerCertificateCustomValidationCallback =
                            (httpRequestMessage, cert, certChain, policyErrors) => true
                    };
                });

            return services;
        }
    }
}
