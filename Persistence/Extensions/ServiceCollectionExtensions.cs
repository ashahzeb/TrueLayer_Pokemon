using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services) => services
            .AddRepositories()
            .AddServices()
            .AddQueries();

        private static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services;

        private static IServiceCollection AddServices(this IServiceCollection services) =>
            services;

        private static IServiceCollection AddQueries(this IServiceCollection services) =>
            services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
    }
}