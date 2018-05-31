using System;
using System.Collections.Generic;
using System.Text;
using WeatherStation.Interfaces;
using RestSharp;
using QuickType;
using System.Configuration;


namespace WeatherStation.Classes
{
    public class WeatherData : Observable 
    {
        private float temperature { get; set; }
        private float humidity { get; set; }
        private float pressure { get; set; }

        public WeatherData()
        {
        }


        public void MeasurementsChanged()
        {
            SetChanged();
            NotifyObservers();
        }

        public void SetMeasurements()
        {

            var weatherStationData = WeatherStationData.FromJson(CallService("http://api.openweathermap.org/data/2.5/weather?zip=33511,us&appid=16b2ef647f259a880fa718334c6f0c16&units=imperial"));

            temperature = weatherStationData.Main.Temp;
            humidity = weatherStationData.Main.Humidity;
            pressure = weatherStationData.Main.Pressure;
            MeasurementsChanged();
        }

        public void SetMeasurements(float temp, float hum, float pres)
        {

            temperature = temp;
            humidity = hum;
            pressure = pres;

            MeasurementsChanged();

        }

        protected string CallService(string url)
        {
            RestClient client = new RestClient(url);
            IRestResponse response = client.Execute(new RestRequest());
            return response.Content;

        }

        public float GetTemperature()
        {
            return temperature;
        }

        public float GetHumidity()
        {
            return humidity;
        }

        public float GetPressure()
        {
            return pressure;
        }
    }
}
