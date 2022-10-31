using AcmeCompany.SmartHR.Application.Extensions;
using AcmeCompany.SmartHR.Infrastructure.Extensions;
using Hangfire;
using Hangfire.SqlServer;
using Scheduler.Api.Mediator;
using Scheduler.Api.Services;
using Scheduler.Api.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationExtensions()
    .AddInfrastructureExtensions(builder.Configuration, builder.Environment);

builder.Services.AddTransient<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddScoped<LoggerManagement>();
builder.Services.AddScoped<HangfireBridge>();

// Add Hangfire services.
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("Default"), new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    }));

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHangfireDashboard();

app.Run();
