var baseAiServiceController = ["$scope", "$http", function ($scope, $http) {
    $scope.comment = "";
    $scope.chat = [];
    $scope.aiResponseData = "";
    $scope.forecastResult = "";
    $scope.aiExecutionTime = "";
    $scope.forecastExecutionTime = "";

    $scope.sendText = function () {
        $scope.chat.unshift({
            self: true,
            msgs: [$scope.comment],
            aiResponse: ""
        });

        $http.post($scope.askPath, { question: $scope.comment })
            .success(function successCallback(response) {
                var forecast = response.ForecastResult.current;
                var queryLocation = response.ForecastResult.location;
                $scope.aiResponseData = response.AiResponse;
                $scope.forecastResult = response.ForecastResult;
                $scope.aiExecutionTime = response.AiExecutionTime;
                $scope.forecastExecutionTime = response.ForecastExecutionTime;
                if (forecast || queryLocation != null) {
                    var temperature = forecast.temp_c;
                    var realFeeling = forecast.feelslike_c;
                    var humidity = forecast.humidity;
                    var pressure = forecast.pressure_in;
                    var city = queryLocation.name;
                    var region = queryLocation.region;
                    var country = queryLocation.country;
                    addBotChat([
                            city + ", " + region,
                            country,
                            "The temperature is " + temperature + "℃",
                            "But feels like " + realFeeling + "℃",
                            "Humidity is " + humidity + " and pressure is " + pressure,
                        ]);
                } else {
                    addBotChat(["I'm sorry, I didn't get that, can you ask in some other way, please"]);
                }
            }).error(function errorCallback(response, statusCode) {
                $scope.aiResponseData = response.AiResponse;
                $scope.forecastResult = response.ForecastResult;
                $scope.aiExecutionTime = response.AiExecutionTime;
                $scope.forecastExecutionTime = response.ForecastExecutionTime;

                if (statusCode === 400) {
                    addBotChat(["I'm sorry, I didn't get that, can you ask in some other way, please"]);
                }
                else if (statusCode === 404) {
                    addBotChat(["I'm sorry, I don't know that place, can you be more specific or ask about somewhere else"]);
                }
                else {
                    addBotChat(["There was an error, I asssure you we have the best code monkeys working to sort it out"]);
                }
            });
        $scope.comment = "";

        function addBotChat(msgs) {
            $scope.chat.unshift({
                self: false,
                msgs: msgs,
                aiResponse: $scope.aiResponseData,
                forecastResult: $scope.forecastResult,
                aiExecutionTime: $scope.AiExecutionTime,
                forecastExecutionTime: $scope.forecastExecutionTime
            });
        }
    }

    $scope.showAIResponse = function (ai, forecast) {
        if (ai != "") {
            $scope.aiResponseData = ai;   
        }
        if (forecast != "") {
            $scope.forecastResult = forecast;
        }
    }
}];

angular.module("weatherAiDemo").controller('BaseAiServiceController', baseAiServiceController);