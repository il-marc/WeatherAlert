using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;


namespace WeatherAlert
{
    class OWMClient
    {
        private const string APIEndpoint = "https://api.openweathermap.org/data/2.5/forecast";
        private string APIKey;
        private string Language;
        public OWMClient(string APIKey, string language)
        {
            this.APIKey = APIKey;
            this.Language = language;
        }
        public List<OWMForecast> OWMRequestForecastsByName(string name)
        {
            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(String.Empty);
            queryString["q"] = name;
            queryString["units"] = "metric";
            queryString["appid"] = APIKey;
            queryString["lang"] = Language;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(APIEndpoint + "?" + queryString.ToString());
            request.Method = "GET";
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        //TODO: Handle
                        return null;
                    }

                    using (StreamReader responseStream = new StreamReader(response.GetResponseStream()))
                    {
                        string responseData = responseStream.ReadToEnd();
                        return JsonConvert.DeserializeObject<OWMForecasts>(responseData).List;
                    }
                }
            }
            catch (WebException e)
            {
                //TODO: Handle
                return null;
            }
        }
    }
}
