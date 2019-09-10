using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherAlert
{
    class OWMWeatherCondition
    {
        [JsonProperty]
        public short id { get; set; }

        [JsonProperty("main")]
        public string Group { get; set; }

        [JsonProperty]
        public string Description { get; set; }

        [JsonProperty]
        public string Icon { get; set; }

    }
    class OWMSummary
    {
        [JsonProperty("temp")]
        public float Temperature { get; set; }

        [JsonProperty("humidity")]
        public byte Humidity { get; set; }
    }
    class OWMForecast
    {
        [JsonProperty("dt")]
        public long Time { get; set; }
                
        [JsonProperty("main")]
        public OWMSummary Summary { get; set; }

        [JsonProperty("weather")]
        public List<OWMWeatherCondition> WeatherCondition { get; set; }
    }
    class OWMForecasts
    {
        [JsonProperty]
        public List<OWMForecast> List { get; set; }
    }
}
