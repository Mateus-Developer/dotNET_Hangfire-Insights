using AcmeCompany.SmartHR.Application.Services;
using AcmeCompany.SmartHR.Domain.Aggregates.EmployeeAggregate;
using MediatR;

namespace AcmeCompany.SmartHR.Application.CommandHandlers.EmployeeCommandHandlers;

internal sealed class ScheduleEmailCommandHandler : AsyncRequestHandler<ScheduleEmailCommand>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmailService _emailService;

    public ScheduleEmailCommandHandler(IEmployeeRepository employeeRepository, IEmailService emailService)
    {
        _employeeRepository = employeeRepository;
        _emailService = emailService;
    }

    protected override async Task Handle(ScheduleEmailCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId, cancellationToken)
            ?? throw new InvalidOperationException($"Employee ({request.EmployeeId}) not found.");

        await _emailService.SendEmailAsync(employee.Email, employee.FullName, request.Subject);
    }
}
