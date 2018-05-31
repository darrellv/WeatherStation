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
            float lastTemp = 0.0F;
            float lastHumidity = 0.0F;
            float lastPressure = 0.0F;

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (!(DateTime.TryParse(ConfigurationManager.AppSettings["LastTimeCalled"], out DateTime result)))
                lastTimeCalled = DateTime.Now.AddMinutes(-11);
            else
                lastTimeCalled = result;

            if (float.TryParse(ConfigurationManager.AppSettings["LastTemp"], out float tempResult))
                lastTemp = tempResult;

            if (float.TryParse(ConfigurationManager.AppSettings["LastHumidity"], out float humResult))
                lastHumidity = humResult;

            if (float.TryParse(ConfigurationManager.AppSettings["LastPressure"], out float pressureResult))
                lastPressure = pressureResult;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            WeatherData weatherData = new WeatherData();

            CurrentConditionsDisplay currentDisplay = new CurrentConditionsDisplay(weatherData);
            StatisticsDisplay statisticsDisplay = new StatisticsDisplay(weatherData);
            ForecastDisplay forecastDisplay = new ForecastDisplay(weatherData);
            HeatIndexDisplay heatIndexDisplay = new HeatIndexDisplay(weatherData);

            if (lastTimeCalled.AddMinutes(10) < DateTime.Now)
            {

                weatherData.SetMeasurements();

                lastTimeCalled = DateTime.Now;
                lastTemp = weatherData.GetTemperature();
                lastHumidity = weatherData.GetHumidity();
                lastPressure = weatherData.GetPressure();
                config.AppSettings.Settings["LastTimeCalled"].Value = lastTimeCalled.ToString();
                config.AppSettings.Settings["LastTemp"].Value = lastTemp.ToString();
                config.AppSettings.Settings["LastHumidity"].Value = lastHumidity.ToString();
                config.AppSettings.Settings["LastPressure"].Value = lastPressure.ToString();

                config.Save(ConfigurationSaveMode.Modified);
            }
            else
            {
                //Console.WriteLine("Wait {0} more minutes and then try again.", (lastTimeCalled.AddMinutes(11) - DateTime.Now).Minutes.ToString());
                weatherData.SetMeasurements(lastTemp, lastHumidity, lastPressure);

            }


            Console.ReadLine();
        }
    }
}
