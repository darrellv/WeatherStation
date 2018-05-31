using System;
using System.Collections.Generic;
using System.Text;
using WeatherStation.Interfaces;

namespace WeatherStation.Classes
{
    public class Observable : IObservable
    {
        private List<IObserver> observers;
        private bool changed = false;

        public Observable()
        {
            observers = new List<IObserver>();
        }

        public void AddObserver(IObserver o)
        {
            observers.Add(o);
        }

        public void DeleteObserver(IObserver o)
        {
            observers.Remove(o);
        }

        public void SetChanged()
        {
            changed = true;
        }

        public void NotifyObservers(Object arg)
        {
            if (changed)
            {
                foreach (IObserver o in observers)
                {
                    o.Update(this, arg);
                }
                changed = false;
            }
        }

        public void NotifyObservers()
        {
            if (changed)
            {
                foreach (IObserver o in observers)
                {
                    o.Update(this, null);
                }
                changed = false;
            }
        }
    }
}
