using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Q101.NetCoreHttpRequestHelper.Abstract;
using Q101.NetCoreHttpRequestHelper.Concrete;
using Q101.NetCoreHttpRequestHelper.Converters.Abstract;

namespace Q101.NetCoreHttpRequestHelper.Extensions
{
    /// <summary>
    /// Service collection extensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register http request helper
        /// </summary>
        /// <param name="services"></param>
        /// <param name="disableSslCheck"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterHttpRequestHelper(this IServiceCollection services,
                                                                   bool disableSslCheck = false)
        {
            services.AddScoped<IJsonConverterAdapter, IJsonConverterAdapter>();

            if (!disableSslCheck)
            {
                services.AddHttpClient<IHttpRequestHelper, HttpRequestHelper>();

                return services;
            }

            // RequestService HttpClient instance injection.
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
