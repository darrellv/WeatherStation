using System;
using System.Collections.Generic;
using System.Text;
using WeatherStation.Interfaces;
using RestSharp;
using QuickType;
using System.Configuration;


namespace WeatherStation.Classes
{
    public class WeatherData : ISubject 
    {
        private List<IObserver> observers;
        private float temperature { get; set; }
        private float humidity { get; set; }
        private float pressure { get; set; }

        public WeatherData()
        {
            observers = new List<IObserver>();
        }

        public void RegisterObserver(IObserver o)
        {
            observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            observers.Remove(o);
        }

        public void NotifyObservers()
        {
            foreach (IObserver o in observers)
            {
                o.Update(temperature, humidity, pressure);
            }
        }

        public void MeasurementsChanged()
        {
            NotifyObservers();
        }

        public void SetMeasurements(DateTime lastTimeCalled)
        {

            var weatherStationData = WeatherStationData.FromJson(CallService("http://api.openweathermap.org/data/2.5/weather?zip=33511,us&appid=16b2ef647f259a880fa718334c6f0c16&units=imperial"));

            temperature = weatherStationData.Main.Temp;
            humidity = weatherStationData.Main.Humidity;
            pressure = weatherStationData.Main.Pressure;

            MeasurementsChanged();

        }
        protected string CallService(string url)
        {
            RestClient client = new RestClient(url);
            IRestResponse response = client.Execute(new RestRequest());
            return response.Content;

        }
    }
}
