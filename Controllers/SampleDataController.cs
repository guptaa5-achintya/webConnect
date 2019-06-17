using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace webConnect.Controllers
{
    public class SampleDataController : AbstractController
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //[HttpGet("[action]")]
        // public IEnumerable<WeatherForecast> WeatherForecasts()
        // {
        //     var rng = new Random();
        //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //     {
        //         DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
        //         TemperatureC = rng.Next(-20, 55),
        //         Summary = Summaries[rng.Next(Summaries.Length)]
        //     });
        // } 

        [HttpPost("WeatherForecasts")] 
        public IEnumerable<WeatherForecast> WeatherForecasts([FromBody] WeatherForecast obj){
            // var obj = JsonConvert.DeserializeObject<WeatherForecast>(jsonObjString); 
            return Enumerable.Range(1, 5).Select(index => {
                obj.Key = index;
                return obj;
                });
        }

        public class WeatherForecast
        {
            public int Key { get; set; }
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
