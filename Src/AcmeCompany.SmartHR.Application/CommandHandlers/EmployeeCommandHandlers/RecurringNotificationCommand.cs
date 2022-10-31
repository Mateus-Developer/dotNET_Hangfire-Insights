using MediatR;

namespace AcmeCompany.SmartHR.Application.CommandHandlers.EmployeeCommandHandlers;

public sealed record RecurringNotificationCommand(
    Guid EmployeeId,
    string Subject,
    string Cron
) : IRequest;
