using Scheduler.Api.Models;

namespace Scheduler.Api.Services;

internal sealed class WeatherForecastService : IWeatherForecastService
{
    private readonly ILogger<WeatherForecastService> _logger;
    private IEnumerable<WeatherForecast> _weatherForecasts = default!;
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public WeatherForecastService(ILogger<WeatherForecastService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async ValueTask<IEnumerable<WeatherForecast>> GetForecastAsync() 
        => await Task.FromResult(_weatherForecasts);

    public async ValueTask UpdateWeatherForecastAsync()
    {
        _weatherForecasts = FetchWeatherForecast();

        _logger.LogInformation("Weather Forecast updated at: {Datetime}", DateTime.Now);

        await Task.CompletedTask;
    }

    private IEnumerable<WeatherForecast> FetchWeatherForecast()
        => Enumerable.Range(1, 5)
            .Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
}
