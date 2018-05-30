using System;
using WeatherStation.Classes;
using System.Configuration;

namespace WeatherStation
{
    class Program
    {
        static void Main(string[] args)
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //get last time called from config file

            //load custom config file
            DateTime lastTimeCalled;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (!(DateTime.TryParse(ConfigurationManager.AppSettings["LastTimeCalled"], out DateTime result)))
                lastTimeCalled = DateTime.Now.AddMinutes(-11);
            else
                lastTimeCalled = result;
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (lastTimeCalled.AddMinutes(10) < DateTime.Now)
            {
                WeatherData weatherData = new WeatherData();

                CurrentConditionsDisplay currentDisplay = new CurrentConditionsDisplay(weatherData);
                StatisticsDisplay statisticsDisplay = new StatisticsDisplay(weatherData);
                ForecastDisplay forecastDisplay = new ForecastDisplay(weatherData);

                weatherData.SetMeasurements(lastTimeCalled);

                lastTimeCalled = DateTime.Now;
                config.AppSettings.Settings["LastTimeCalled"].Value = lastTimeCalled.ToString();

                config.Save(ConfigurationSaveMode.Modified);
            }

            Console.ReadLine();
        }
    }
}
