using AcmeCompany.SmartHR.Application.Services;
using AcmeCompany.SmartHR.Domain.Aggregates.EmployeeAggregate;
using MediatR;

namespace AcmeCompany.SmartHR.Application.CommandHandlers.EmployeeCommandHandlers;

internal sealed class UrgentNotificationCommandHandler : AsyncRequestHandler<UrgentNotificationCommand>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmailService _emailService;

    public UrgentNotificationCommandHandler(IEmployeeRepository employeeRepository, IEmailService emailService)
    {
        _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }

    protected override async Task Handle(UrgentNotificationCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId, cancellationToken)
            ?? throw new InvalidOperationException($"Employee ({request.EmployeeId}) not found.");

        await _emailService.SendEmailAsync(employee.Email, employee.FullName, request.Subject);
    }
}