using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWomenApp.Database.Models.WeatherApi
{
    public class WeatherDashboardViewModel
    {
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
        public double? WindSpeed { get; set; }
        public double? Pressure { get; set; }
        public string Symbol { get; set; }
    }
}
