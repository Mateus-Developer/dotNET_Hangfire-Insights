using MediatR;

namespace AcmeCompany.SmartHR.Application.CommandHandlers.EmployeeCommandHandlers;

public sealed record ScheduleEmailCommand(
    Guid EmployeeId,
    string Subject,
    DateTime SchedulerAt
) : IRequest;
