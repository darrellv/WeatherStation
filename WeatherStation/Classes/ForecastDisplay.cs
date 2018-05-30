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
        private ISubject weatherData;

        public ForecastDisplay(ISubject wd)
        {
            weatherData = wd;
            weatherData.RegisterObserver(this);


        }

        public void Update(float temp, float hum, float pres)
        {
            temperature = temp;
            humidity = hum;
            Display();
        }

        public void Display()
        {
            Console.WriteLine("Forecast: {0}F degrees and {1}% humidity", temperature, humidity);
        }
    }
}
