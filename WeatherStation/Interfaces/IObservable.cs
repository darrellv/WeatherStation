using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherStation.Interfaces
{
    public interface IObservable
    {
        void AddObserver(IObserver o);
        void DeleteObserver(IObserver o);
        void NotifyObservers();
        void NotifyObservers(Object arg);
        void SetChanged();
    }
}
