# WeatherStation
Observer Design Pattern

This application demonstrates the Observer Design Pattern.

It uses a subject object that gathers real-time weather data about Brandon, Florida and reports it to a group of observer objects that then display the required information.

Because of limitations on how often the web service to gather the information can be called, a config file holds the last time the web service was called.  They program reads this time and won't call the webservice again unless 10 minutes has elapsed from the last time it was called.
