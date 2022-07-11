using MyApp.Models;
using System;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecast[]> GetForecastAsync(DateTime startDate);
    }
}