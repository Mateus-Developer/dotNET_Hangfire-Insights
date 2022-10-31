using Scheduler.Api.Models;

namespace Scheduler.Api.Services;

public interface IWeatherForecastService
{
    ValueTask UpdateWeatherForecastAsync();
    ValueTask<IEnumerable<WeatherForecast>> GetForecastAsync();
}
