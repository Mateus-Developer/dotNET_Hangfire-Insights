using AcmeCompany.SmartHR.Domain.Aggregates.EmployeeAggregate;
using AcmeCompany.SmartHR.Domain.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcmeCompany.SmartHR.Application.CommandHandlers.EmployeeCommandHandlers;

internal sealed class HireEmployeeCommandHandler : IRequestHandler<HireEmployeeCommand, Guid>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<HireEmployeeCommandHandler> _logger;

    public HireEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork, ILogger<HireEmployeeCommandHandler> logger)
    {
        _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Guid> Handle(HireEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee(
            request.FullName,
            request.Email,
            request.HireDate,
            request.JobTitle,
            request.Salary
        );

        await _employeeRepository.CreateAsync(employee, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Employee {EmployeeId} registered successfully.", employee.Id);

        return employee.Id;
    }
}
