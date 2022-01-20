"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.WeatherData = void 0;
/**
 * todo: model class for get weather API.
 * */
var WeatherData = /** @class */ (function () {
    function WeatherData() {
        this.CityName = "";
        this.Latitude = 0;
        this.Longitude = 0;
        this.UtcDateTime = "";
        this.UtcDateTimeLongFormat = "";
        this.WindDegree = 0;
        this.WindSpeed = 0;
        this.Visibility = 0;
        this.Farenheit = 0;
        this.Celcius = 0;
        this.SkyConditions = "";
    }
    return WeatherData;
}());
exports.WeatherData = WeatherData;
//# sourceMappingURL=weather-data.js.map