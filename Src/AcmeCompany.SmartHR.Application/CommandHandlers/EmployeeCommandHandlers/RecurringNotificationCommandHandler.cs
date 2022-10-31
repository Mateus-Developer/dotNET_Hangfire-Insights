using AcmeCompany.SmartHR.Application.Services;
using AcmeCompany.SmartHR.Domain.Aggregates.EmployeeAggregate;
using MediatR;

namespace AcmeCompany.SmartHR.Application.CommandHandlers.EmployeeCommandHandlers;

internal sealed class RecurringNotificationCommandHandler : AsyncRequestHandler<RecurringNotificationCommand>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmailService _emailService;

    public RecurringNotificationCommandHandler(IEmployeeRepository employeeRepository, IEmailService emailService)
    {
        _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }

    protected override async Task Handle(RecurringNotificationCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId, cancellationToken)
            ?? throw new InvalidOperationException($"Employee ({request.EmployeeId}) not found.");

        await _emailService.SendEmailAsync(employee.Email, employee.FullName, request.Subject);
    }
}
