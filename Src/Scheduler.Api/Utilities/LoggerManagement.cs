namespace Scheduler.Api.Utilities;

public class LoggerManagement
{
    private readonly ILogger<LoggerManagement> _logger;

    public LoggerManagement(ILogger<LoggerManagement> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void LoggerJob(string jobId)
    {
        _logger.LogInformation("continuation from {JobId}", jobId);
    }
}
