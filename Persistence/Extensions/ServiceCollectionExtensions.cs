using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services) => services
            .AddRepositories()
            .AddQueries();

        private static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services
                .AddSingleton<IPokemonRepository, PokemonRepository>()
                .AddSingleton<ITranslatorRepository, TranslatorRepository>();

        private static IServiceCollection AddQueries(this IServiceCollection services) =>
            services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
    }
}