function WeatherModel() {

    var errorMessage = "Oops! An error has occurred! Please try again later.";

    var self = this;
    self.errorMessage = ko.observable("");

    self.weatherVM = {
        Temperature: ko.observable("DegreeCelsius"),
        WindSpeed: ko.observable("MilePerHour"),
        Location: ko.observable("")
    }

    self.apiResult = {
        Temperature: ko.observable(),
        WindSpeed: ko.observable(),
        LastUpdated: ko.observable(),
        Location: ko.observable(),
        valid: ko.observable(false)
    }

    self.resultVisible = ko.computed(function() {
        return self.errorMessage() || self.apiResult.LastUpdated();
    });

    self.getWeather = function () {

        self.errorMessage(null);
        self.apiResult.valid(false);

        if (self.weatherVM.Location()) {

            var model = ko.toJS(self.weatherVM);

            var request = $.ajax({
                url: '/api/weather',
                type: "GET",
                //data: JSON.stringify(model),
                data: $.param(model),
                //contentType: "application/json"
            });

            request.done(function (data) {

                if (data) {
                    self.apiResult.Temperature(data.Temperature);
                    self.apiResult.WindSpeed(data.WindSpeed);
                    self.apiResult.Location(data.Location);
                    self.apiResult.LastUpdated(data.CreatedStamp);
                    self.apiResult.valid(true);
                } else {
                    self.errorMessage("");
                }

            });

            request.error(function (data) {
                self.errorMessage(errorMessage);
            });
        } else {
            self.errorMessage(errorMessage);
        }
    }
}

ko.applyBindings(new WeatherModel());