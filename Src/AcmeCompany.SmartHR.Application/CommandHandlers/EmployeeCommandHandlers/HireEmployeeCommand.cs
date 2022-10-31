using MediatR;

namespace AcmeCompany.SmartHR.Application.CommandHandlers.EmployeeCommandHandlers;

public sealed record HireEmployeeCommand(
    string FullName,
    string Email,
    DateTime HireDate,
    string JobTitle,
    decimal Salary
) : IRequest<Guid>;
