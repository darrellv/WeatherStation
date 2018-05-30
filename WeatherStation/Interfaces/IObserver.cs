using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherStation.Interfaces
{
    public interface IObserver
    {
        void Update(float temp, float humidity, float pressure);
    }
}
