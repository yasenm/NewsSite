APP['Weather'] = {};
APP.Weather = (function () {
    var vratsaENName = 'Vratsa',
        montanaENName = 'Montana',
        vidinENName = 'Vidin',
        sofiaENName = 'Sofia',
        weatherUrlMode = ',bg&lang=bg&mode=html',
        weatherAPIUrl = 'http://api.openweathermap.org/data/2.5/weather?q=',
        weatherChangeBtnSelector = '.weather-town-for-town-btn',
        weatherChangeSelectDropDownList = '#weather-select-town',

        _getWeatherByTownName = function (townName) {
            return APP.HttpRequester.getHTML(weatherAPIUrl + townName + weatherUrlMode);
        },

        _setEventsForWeather = function () {
            $(weatherChangeSelectDropDownList).on('change', function () {
                var $this = $(this);
                var townName = $this.val();
                var townNameBG = $('option[value=' + townName + ']').text();
                _getWeatherByTownName(townName)
                    .success(function (townHtml) {
                        $('#hidden-weather').html(townHtml);

                        var image = $('div [title="Titel"]').html();
                        var degreesContainerText = $('div [title="Current Temperature"]').text();
                        if(degreesContainerText.length > 4)
                            degreesContainerText = degreesContainerText.substring(0, 4) + degreesContainerText.charAt(degreesContainerText.length - 1);
                        var clouds = $('#hidden-weather div > div:contains("Clouds:")').text().substring(7).trim();
                        var humidity = $('#hidden-weather div > div:contains("Humidity:")').text().substring(9).trim();
                        var wind = $('#hidden-weather div > div:contains("Wind:")').text().substring(5).trim();
                        var pressure = $('#hidden-weather div > div:contains("Pressure:")').text().substring(9).trim();

                        $('#weather-img-container').html(image);
                        $('#degrees-container strong').text(degreesContainerText);
                        $('#clouds-container strong').text(clouds);
                        $('#humidity-container strong').text(humidity);
                        $('#wind-container strong').text(wind);
                        $('#pressure-container strong').text(pressure);
                        $('#city-name-container strong').text(townNameBG);
                    })
            })

            $(weatherChangeBtnSelector).click(function () {
                var $this = $(this);
                var townName = $this.data('town-name');
                _getWeatherByTownName(townName)
                    .success(function (success) {
                        $('.weather-container').html(success);
                    })
            })
        }

    return {
        init: function () {
            _setEventsForWeather();
            $(weatherChangeSelectDropDownList).val('Vratsa').trigger('change');
        }
    }
}());