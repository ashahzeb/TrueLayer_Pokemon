using System;
using Infrastructure.Configuration;
using Infrastructure.HttpClients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddConfiguration<IHttpClientConfiguration, HttpClientConfiguration>(configuration);
            
            services.AddHttpClient<IPokemonApiHttpClient, PokemonApiHttpClient>((provider, client) =>
                client.BaseAddress = new Uri(provider.GetService<IHttpClientConfiguration>().PokemonApiBaseUrl));
                
            services.AddHttpClient<ITranslationApiHttpClient, TranslationApiHttpClient>((provider, client) =>
                client.BaseAddress = new Uri(provider.GetService<IHttpClientConfiguration>().TranslatorApiBaseUrl));

                return services;
        }
        
        private static IServiceCollection AddConfiguration<TIConfiguration, TConfiguration>(this IServiceCollection serviceCollection, IConfiguration configuration)
            where TIConfiguration : class
            where TConfiguration : class, TIConfiguration, new()
            => serviceCollection.AddConfiguration<TIConfiguration, TConfiguration>(configuration, typeof(TConfiguration).Name);

        private static IServiceCollection AddConfiguration<TIConfiguration, TConfiguration>(this IServiceCollection serviceCollection, IConfiguration configuration, string configurationSectionName)
            where TIConfiguration : class
            where TConfiguration : class, TIConfiguration, new()
            => serviceCollection
                .Configure<TConfiguration>(configuration.GetSection(configurationSectionName))
                .AddSingleton<TIConfiguration>(provider => provider.GetRequiredService<IOptions<TConfiguration>>().Value);
    }
}