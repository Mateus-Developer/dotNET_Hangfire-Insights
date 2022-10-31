namespace AcmeCompany.SmartHR.Application.ViewModels;

public sealed record EmployeeDisplay
{
    public Guid Id { get; private init; }
    public string FullName { get; private init; } = default!;
    public string Email { get; private init; } = default!;
    public string JobTitle { get; private init; } = default!;
    public decimal Salary { get; private init; } = default!;
    public DateTime HireDate { get; private init; }

    private EmployeeDisplay() { }
}
