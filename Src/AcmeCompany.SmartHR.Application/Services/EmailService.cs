using Microsoft.Extensions.Logging;

namespace AcmeCompany.SmartHR.Application.Services;

internal sealed class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async ValueTask SendEmailAsync(string email, string emailTo, string subject)
    {
        _logger.LogInformation("Sended email ({Email}) to {EmailTo} with subject {Subject} at {Datetime}",
            email,
            emailTo,
            subject,
            DateTime.Now);

        await Task.CompletedTask;
    }
}

