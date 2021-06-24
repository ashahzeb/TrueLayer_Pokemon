using Infrastructure.RequestBehaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatorRequestLogging(this IServiceCollection services) =>
            services
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingPipelineBehavior<,>));
    }
}