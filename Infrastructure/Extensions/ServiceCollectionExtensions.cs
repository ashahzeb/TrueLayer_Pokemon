using System;
using Infrastructure.Configuration;
using Infrastructure.HttpClients;
using Infrastructure.RequestBehaviors;
using MediatR;
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
            services.AddMediatorRequestLogging()
                .AddConfiguration<IHttpClientConfiguration, HttpClientConfiguration>(configuration)
                .AddTransient<IPokemonApiHttpClient, PokemonApiHttpClient>()
                .AddTransient<ITranslationApiHttpClient, TranslationApiHttpClient>();

            services.AddHttpClient<PokemonApiHttpClient>((provider, client) =>
                client.BaseAddress = new Uri(provider.GetService<IHttpClientConfiguration>().PokemonApiBaseUrl));
                
            services.AddHttpClient<TranslationApiHttpClient>((provider, client) =>
                client.BaseAddress = new Uri(provider.GetService<IHttpClientConfiguration>().TranslatorApiBaseUrl));

            return services;
        }

        

        private static IServiceCollection AddMediatorRequestLogging(this IServiceCollection services) =>
            services
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingPipelineBehavior<,>));

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