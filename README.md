# Readme
### Planning
The first thing I did was spend 15 minutes reading the spec and noting down what I needed to implement using Trello.
https://trello.com/b/VwlbtmEs/weather-app
### Project Setup
 
* Created a new WebApi project.
* Created a new unit tests project.
* Added a services class library project.

### Packages

* Added RestSharp to the class library project to call out to the weather apis.
* Added Ninject to the WebApi project for dependency injection.
* Added XUnit, Moq & FluentAssertations to the test project.

### Service

* Created Accuweather and BBCWeather service classes to call the external APi's and result types to handle the returned data.
* Implemented GetWeather method on the service classes.
* Refactored service classes and result types to use base classes as a lot of the functionality was shared.
* Added in a debug line for weather api exceptions.
* Added a timeout to the weather api requests.
* Bound the service implementations to an IWeatherService interface using ninject.

### Controller

* Added a weather controller.
* Injected in a collection of IWeatherServices as a constructor argument.
* Called GetWeather on each weatherService, storing the result if not null.
* Added a Get API endpoint & getweather model.

### Units of measurement

* Added units.net library to do some of the heavy lifting regarding units of measurement.
* Added windspeed/temperature values & unit types to the weather result classes.
* Added conversion methods to the base result class.

### Extensions

* Added an extension method to a collection of weatherResults to standardize the temp & wind speed based on user input values. It then averages the weather result temperature & wind speed.

### Client side

* Installed knockout.js package and added models for user input & api responses.
* Added an Ajax GET method to get weather data.
* Validated a location is selected.
* Updated the view on success / error callbacks.
 
### Notes

* Development Time 5-6 Hours including refactoring.
* There was a period between this where I spent a few hours attempting to use async /await with restClient but had issues which I think were due to covariant types. I also do not have much experiencing unit testing async methods and conceded that this wasn’t the time for a learning experience.
 
### Further work

* Unit test Javascript. I have used Jasmine + Chutzpah before, but I am still learning to use it.
* Call the external weather API’s concurrently – I attempted this with async/await but still have more to learn.
* Implement caching on location/weather data, the bbc/accuweather are updated around once an hour. Use an in proc cache e.g. httpruntimecache for a quick test and something like redis for a scalable solution.
* Log the httpExceptions from weather services instead of writing them to debug.
* Set the weather api timeout property as an app key.

### Refactoring

* The only thing I would refactor further is - I think unit of measurement conversion should be in its own service class. Currently it is split between extension methods and instance methods on the result objects. This would be better suited in a service and would also mean I could remove the WeatherResult conversion methods which have side effects.
