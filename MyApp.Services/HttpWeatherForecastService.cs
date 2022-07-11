using Microsoft.Extensions.Options;
using MyApp.Models;
using MyApp.Models.Opts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public class HttpWeatherForecastService : IWeatherForecastService
    {
        private readonly HttpClient _client;
        //private readonly IOptions<ApiOptions> _options;

        public HttpWeatherForecastService(HttpClient client) //IOptions<ApiOptions> options
        {
            this._client = client;
            // Résilience Http manuelle
            //this._options = options;
        }
        public async Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            try
            {
                var response = await _client.GetAsync(requestUri: "weatherforecast"); // weatherforecast est la route de notre WeatherForecast (préfixe du controller)
                #region Résilience http manuelle
                //int retry = 0;
                //while (!response.IsSuccessStatusCode)
                //{
                //    await Task.Delay(_options.Value.Delay);
                //    response = await _client.GetAsync(requestUri: "weatherforecast");
                //    retry++;
                //    if (retry >= _options.Value.RetryMax)
                //    {
                //        break;
                //    }
                //} 
                #endregion

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var jsonOptions = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    return JsonSerializer.Deserialize<WeatherForecast[]>(jsonData, jsonOptions);
                }
            }
            catch (Exception)
            {

                // je loggue
            }
            return new WeatherForecast[0];
        }
    }
}
