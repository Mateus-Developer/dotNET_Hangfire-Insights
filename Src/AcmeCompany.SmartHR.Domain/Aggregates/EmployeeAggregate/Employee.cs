using AcmeCompany.SmartHR.Domain.SeedWork;

namespace AcmeCompany.SmartHR.Domain.Aggregates.EmployeeAggregate;

public class Employee : Entity, IAggregateRoot
{
    public string FullName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public DateTime HireDate { get; private set; }
    public string JobTitle { get; private set; } = null!;
    public decimal Salary { get; private set; }

    public Employee(string fullName, string email, DateTime hireDate, string jobTitle, decimal salary)
    {
        if (string.IsNullOrEmpty(fullName))
            throw new ArgumentException($"'{nameof(fullName)}' cannot be null or empty.", nameof(fullName));

        if (string.IsNullOrEmpty(email))
            throw new ArgumentException($"'{nameof(email)}' cannot be null or empty.", nameof(email));

        if (string.IsNullOrEmpty(jobTitle))
            throw new ArgumentException($"'{nameof(jobTitle)}' cannot be null or empty.", nameof(jobTitle));

        FullName = fullName;
        Email = email;
        HireDate = hireDate >= DateTime.UtcNow
            ? hireDate
            : throw new ArgumentException($"'{nameof(HireDate)}' cannot be less or equals than current date.");
        JobTitle = jobTitle;
        Salary = salary > 0 ? salary : throw new ArgumentException($"'{nameof(Salary)}' cannot be less or equals than 0.");
    }

    private Employee() { }
}
