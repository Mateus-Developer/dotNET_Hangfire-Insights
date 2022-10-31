using Hangfire;
using MediatR;
using Scheduler.Api.Mediator;

namespace Scheduler.Api.Extensions;

public static class MediatorExtensions
{
    /// <summary>
    /// The Fire-and-Forget.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="mediator"></param>
    /// <param name="jobName"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    public static string Enqueue<TRequest>(this IMediator mediator, string jobName, TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IBaseRequest
    {
        return BackgroundJob.Enqueue<HangfireBridge>(
            b => b.Send(jobName, request, cancellationToken));
    }

    /// <summary>
    /// Delay Job.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="mediator"></param>
    /// <param name="jobName"></param>
    /// <param name="request"></param>
    /// <param name="scheduleAt"></param>
    /// <param name="cancellationToken"></param>
    public static string Scheduler<TRequest>(this IMediator mediator, string jobName, TRequest request, DateTime scheduleAt, CancellationToken cancellationToken = default)
        where TRequest : IBaseRequest
    {
        return BackgroundJob.Schedule<HangfireBridge>(
            hb => hb.Send(jobName, request, cancellationToken),
            scheduleAt);
    }

    /// <summary>
    /// Recurring Job.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="mediator"></param>
    /// <param name="jobName"></param>
    /// <param name="request"></param>
    /// <param name="cron"></param>
    /// <param name="cancellationToken"></param>
    public static void Recurring<TRequest>(this IMediator mediator, string jobName, TRequest request, string cron, CancellationToken cancellationToken = default)
        where TRequest: IBaseRequest
    {
        RecurringJob.AddOrUpdate<HangfireBridge>(
            jobName,
            hb => hb.Send(jobName, request, cancellationToken),
            cron);
    }
}
