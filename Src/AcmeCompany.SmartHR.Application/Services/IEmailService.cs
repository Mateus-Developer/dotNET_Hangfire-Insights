namespace AcmeCompany.SmartHR.Application.Services;

public interface IEmailService
{
    ValueTask SendEmailAsync(string email, string emailTo, string subject);
}

