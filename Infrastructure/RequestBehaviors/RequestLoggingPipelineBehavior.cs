using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;

namespace Infrastructure.RequestBehaviors
{
    public class RequestLoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;

        public RequestLoggingPipelineBehavior(ILogger logger)
        {
            _logger = logger.ForContext<RequestLoggingPipelineBehavior<TRequest, TResponse>>();
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.Information("Starting executing {@Request}", request);

            var stopwatch = Stopwatch.StartNew();
            var response = await next().ConfigureAwait(false);

            stopwatch.Stop();

            _logger.Information("Done executing {@Request}. Took: {Time} ms", request, stopwatch.ElapsedMilliseconds);

            return response;
        }
    }
}