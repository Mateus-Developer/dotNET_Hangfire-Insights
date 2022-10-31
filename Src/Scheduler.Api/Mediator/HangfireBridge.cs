using MediatR;
using System.ComponentModel;

namespace Scheduler.Api.Mediator;

public sealed class HangfireBridge
{
    private readonly IMediator _mediator;

    public HangfireBridge(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [DisplayName("{0}")]
    public async Task Send<TRequest>(string jobName, TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IBaseRequest
    {
        await _mediator.Send(request, cancellationToken);
    }
}
