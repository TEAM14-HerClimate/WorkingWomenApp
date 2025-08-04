using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingWomenApp.Database.Core;
using WorkingWomenApp.Database.Models.WeatherApi;

namespace WorkingWomenApp.BLL.Interfaces
{
    public interface IWeatherApiService
    {
        Task<(bool, string, WeatherForecast)> GetWeatherInfo(double latitude, double longitude);
    }

   
}


