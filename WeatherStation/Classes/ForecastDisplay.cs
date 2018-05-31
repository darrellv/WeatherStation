using System;
using System.Collections.Generic;
using System.Text;
using WeatherStation.Interfaces;

namespace WeatherStation.Classes
{
    class ForecastDisplay : IObserver, IDisplayElement
    {
        private float temperature { get; set; }
        private float humidity { get; set; }
        private Observable observable;

        public ForecastDisplay(Observable obs)
        {
            observable = obs;
            observable.AddObserver(this);

        }

        public void Update(Observable obs, Object arg)
        {
            if (obs is WeatherData)
            {
                WeatherData wd = (WeatherData)obs;

                temperature = wd.GetTemperature();
                humidity = wd.GetHumidity();
                Display();
            }
        }

        public void Display()
        {
            Console.WriteLine("Forecast: {0}F degrees and {1}% humidity", temperature, humidity);
        }
    }
}
