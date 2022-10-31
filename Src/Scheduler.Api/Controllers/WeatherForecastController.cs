using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Services;
using Scheduler.Api.Utilities;

namespace Scheduler.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _weatherForecastService;

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(IWeatherForecastService weatherForecastService,
                                     ILogger<WeatherForecastController> logger)
    {
        _weatherForecastService = weatherForecastService ?? throw new ArgumentNullException(nameof(weatherForecastService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost("[action]")]
    public IActionResult FireAndForgetWeatherForecast()
    {
        return Ok(BackgroundJob.Enqueue(
            () => _weatherForecastService.UpdateWeatherForecastAsync()));
    }
    
    [HttpPost("[action]")]
    public IActionResult ScheduleWeatherForecast()
    {
        return Ok(BackgroundJob.Schedule(
            () => _weatherForecastService.UpdateWeatherForecastAsync(),
            DateTime.Now.AddMinutes(1)));
    }

    [HttpPost("[action]")]
    public IActionResult RecurringUpdateWeatherForecast()
    {
        RecurringJob.AddOrUpdate(
            () => _weatherForecastService.UpdateWeatherForecastAsync(),
            Cron.Minutely);

        return NoContent();
    }

    [HttpPost("[action]")]
    public IActionResult ScheduleContinuationWeatherForecast()
    {
        var jobId = BackgroundJob.Schedule(
            () => _weatherForecastService.UpdateWeatherForecastAsync(),
            DateTime.Now.AddMinutes(1));
        var jobContinuationId = BackgroundJob.ContinueJobWith<LoggerManagement>(
            jobId, 
            lm => lm.LoggerJob(jobId));

        return Ok($"The job ({jobId}) scheduler with continuation ({jobContinuationId})");
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async ValueTask<IActionResult> Get()
    {
        return Ok(await _weatherForecastService.GetForecastAsync());
    }
}