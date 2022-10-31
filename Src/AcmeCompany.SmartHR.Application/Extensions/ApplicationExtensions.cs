using AcmeCompany.SmartHR.Application.QueryHandlers.Common;
using AcmeCompany.SmartHR.Application.QueryHandlers.EmployeeQuerys;
using AcmeCompany.SmartHR.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AcmeCompany.SmartHR.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationExtensions(this IServiceCollection services)
    {
        services.AddSingleton<IConnectionFactory, ConnectionFactory>();
        services.AddTransient<IEmployeeQueryHandler, EmployeeQueryHandler>();
        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}
