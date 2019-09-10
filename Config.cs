using Newtonsoft.Json;
using System;
using System.IO;

namespace WeatherAlert
{
    class Config
    {
        [JsonProperty]
        public readonly string OWMAPIKey = "";
        [JsonProperty]
        public readonly string City = "Odessa,UA";
        [JsonProperty]
        public readonly string Lang = "ru";
        [JsonProperty]
        public readonly string GmailUsername = "";
        [JsonProperty]
        public readonly string GmailPassword = "";

        private Config() { }

        public static Config Load(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                File.WriteAllText(filePath, JsonConvert.SerializeObject(new Config()));
                return null;
            }
            Config Config;
            try
            {
                Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(filePath));
            }
            catch (Exception e)
            {
                //TODO: Handle
                return null;
            }
            return Config;
        }

    }
}
