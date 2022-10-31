using AcmeCompany.SmartHR.Domain.Aggregates.EmployeeAggregate;
using AcmeCompany.SmartHR.Domain.SeedWork;
using AcmeCompany.SmartHR.Infrastructure.Persistence;
using AcmeCompany.SmartHR.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace AcmeCompany.SmartHR.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructureExtensions(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment)
    {
        services.AddDbContext<SmartHRContext>(options
            => options.UseSqlServer(configuration.GetConnectionString("Default"), options
                    => options.EnableRetryOnFailure(3, TimeSpan.FromSeconds(10), null))
            .EnableDetailedErrors(hostEnvironment.IsDevelopment())
            .EnableSensitiveDataLogging(hostEnvironment.IsDevelopment()));

        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<SmartHRContext>());
        services.AddTransient<IEmployeeRepository, EmployeeRepository>();

        services.AddMediatR(Assembly.Load("AcmeCompany.SmartHR.Application"));

        return services;
    }
}
