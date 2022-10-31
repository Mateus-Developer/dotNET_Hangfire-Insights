using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AcmeCompany.SmartHR.Infrastructure.Persistence;

internal sealed class ECommerceContextFactory : IDesignTimeDbContextFactory<SmartHRContext>
{
    public SmartHRContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SmartHRContext>();
        optionsBuilder.UseSqlServer(args[0]);

        return new SmartHRContext(optionsBuilder.Options);
    }
}
