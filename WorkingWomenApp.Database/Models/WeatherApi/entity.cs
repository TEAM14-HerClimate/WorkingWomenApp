using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WorkingWomenApp.Database.Models.WeatherApi
{
    public class WeatherForecast
    {
        public string Type { get; set; }
        public Geometry Geometry { get; set; }
        public Properties Properties { get; set; }
    }

    public class Geometry
    {
        public string Type { get; set; }
        public List<double> Coordinates { get; set; }
    }

    public class Properties
    {
        public Meta Meta { get; set; }
        public List<TimeSeries> Timeseries { get; set; }
    }

    public class Meta
    {
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public Dictionary<string, string> Units { get; set; }
    }

    public class TimeSeries
    {
        public DateTime Time { get; set; }
        public WeatherData Data { get; set; }
    }

    public class WeatherData
    {
        public Instant Instant { get; set; }

        [JsonPropertyName("next_1_hours")]
        public ForecastPeriod Next1Hours { get; set; }

        [JsonPropertyName("next_6_hours")]
        public ForecastPeriod Next6Hours { get; set; }

        [JsonPropertyName("next_12_hours")]
        public ForecastPeriod Next12Hours { get; set; }
    }

    public class Instant
    {
        public Dictionary<string, double> Details { get; set; }
    }

    public class ForecastPeriod
    {
        public Summary Summary { get; set; }
        public Dictionary<string, double> Details { get; set; }
    }

    public class Summary
    {
        [JsonPropertyName("symbol_code")]
        public string SymbolCode { get; set; }
    }
}
