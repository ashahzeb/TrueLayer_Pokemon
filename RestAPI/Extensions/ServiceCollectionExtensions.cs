using Domain.Extensions;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Extensions;

namespace RestAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddDomain()
                .AddPersistence()
                .AddMediatR(typeof(ServiceCollectionExtensions).Assembly)
                .AddMediatorRequestLogging();
    }
}