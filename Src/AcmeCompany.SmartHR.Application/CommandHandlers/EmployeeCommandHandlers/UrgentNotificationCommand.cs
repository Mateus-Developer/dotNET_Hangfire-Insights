using MediatR;

namespace AcmeCompany.SmartHR.Application.CommandHandlers.EmployeeCommandHandlers;

public sealed record UrgentNotificationCommand(
    Guid EmployeeId,
    string Subject
) : IRequest;
