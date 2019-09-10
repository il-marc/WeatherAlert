using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace WeatherAlert
{
    class Program
    {
        static void Main(string[] args)
        {
            Config config = Config.Load("config.json");
            if (config == null)
            {
                Console.WriteLine("Ошибка config.json!");
                Console.ReadLine();
                return;
            }
            OWMClient owm = new OWMClient(config.OWMAPIKey, config.Lang);
            List<OWMForecast> forecasts = owm.OWMRequestForecastsByName(config.City);
            Dictionary<DateTime, string> forecastsByDays = new Dictionary<DateTime, string>();
            foreach (OWMForecast forecast in forecasts)
            {
                DateTime dateTime = DateTimeOffset.FromUnixTimeSeconds(forecast.Time).UtcDateTime;
                string timeOfDay = String.Empty;
                if (dateTime.Hour == 3)
                {
                    timeOfDay = "Ночь";
                }
                else if (dateTime.Hour == 15)
                {
                    timeOfDay = "День";
                }
                else
                {
                    continue;
                }

                string weatherConditions = String.Empty;
                foreach (OWMWeatherCondition condition in forecast.WeatherCondition)
                {
                    weatherConditions += weatherConditions.Equals(String.Empty) ? 
                        condition.Description : (", " + condition.Description);
                }
                string forecastExplained = String.Format("    {0}: {1} C, {2}", timeOfDay, 
                    forecast.Summary.Temperature, weatherConditions);
                if (!forecastsByDays.ContainsKey(dateTime.Date))
                {
                    forecastsByDays[dateTime.Date] = String.Empty;
                }
                forecastsByDays[dateTime.Date] += forecastExplained + Environment.NewLine;
            }
            string fiveDaysForecast = String.Empty;
            foreach (var item in forecastsByDays)
            {
                fiveDaysForecast += item.Key.ToString("dd.MM.yyyy") + Environment.NewLine
                    + item.Value;
            }
            fiveDaysForecast = fiveDaysForecast.Trim();
            string subject = String.Format("Погода на 5 дней для {0}. Текущее время: {1}", 
                config.City, DateTime.Now.ToString("HH:mm:ss"));
            Console.WriteLine(subject);
            Console.WriteLine(fiveDaysForecast);
            
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--window-size=1280,728");
            IWebDriver driver = new ChromeDriver(options);

            var gmail = new GmailPage(driver);

            gmail.SighIn(config.GmailUsername, config.GmailPassword);
            gmail.Send(config.GmailUsername + "@gmail.com", subject, fiveDaysForecast);
            var messageBody = gmail.GetBodyBySubject(subject);

            Console.WriteLine("Тело письма:");
            Console.WriteLine(messageBody);

            Console.WriteLine("Прогноз из письма {0} тому, что получили с openweathermap",
                messageBody.Equals(fiveDaysForecast)?"равен":"не равен");
            driver.Quit();
            Console.ReadLine();
        }
    }
}
