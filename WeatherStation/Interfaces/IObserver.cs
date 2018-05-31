using System;
using System.Collections.Generic;
using System.Text;
using WeatherStation.Classes;

namespace WeatherStation.Interfaces
{
    public interface IObserver
    {
        void Update(Observable o, Object arg);
    }
}
