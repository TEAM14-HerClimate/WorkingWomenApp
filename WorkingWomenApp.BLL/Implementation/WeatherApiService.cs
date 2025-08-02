
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using WorkingWomenApp.BLL.Interfaces;
using WorkingWomenApp.Database.Models.WeatherApi;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WorkingWomenApp.BLL.Implementation
{
    public class WeatherApiService:IWeatherApiService
    {
        public string BaseUrl { get; set; }
        readonly HttpClient client;
        public WeatherApiService()
        {

            BaseUrl = "https://api.met.no";

            client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };

        }

        public async Task<(bool, string, WeatherData)> GetWeatherInfo(double latitude, double longitude)
            {
            string errorMessage = null;
            bool success = false;

            WeatherData resultingMessage = new WeatherData();

            try
            {
               
                var customUserAgent = "MyWeatherApp/1.0 (https://github.com/bonolives/Team14)";
                var request = new HttpRequestMessage(HttpMethod.Get, $"weatherapi/locationforecast/2.0/compact?lat={latitude}&lon={longitude}");


                //request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                //request.Headers.Add("User-Agent", customUserAgent);
                var response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"An error has occurred while verifying business name");

                }
              
                var xmlString = await response.Content.ReadAsStringAsync();
                
                string apiResponse = await response.Content.ReadAsStringAsync();
                resultingMessage = JsonSerializer.Deserialize<WeatherData>(apiResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }); ;

                success = true;

                return (success, errorMessage, resultingMessage);
            }
            catch (Exception e)
            {
                //errorMessage = e.ExtractInnerExceptionMessage();

            }

            return (success, errorMessage, resultingMessage);
        }
    }
}
