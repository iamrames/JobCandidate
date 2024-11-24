using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace JobCandidate.Application.Common.Behaviours;
public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task Process(TRequest request, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation("JobCandidate Request: {Name} {@Request}",
            requestName, request);
    }
}
