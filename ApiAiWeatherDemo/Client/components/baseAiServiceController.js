var baseAiServiceController = ["$scope", "$http", function ($scope, $http) {
    $scope.comment = "";
    $scope.chat = [];
    $scope.aiResponseData = "";

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
                var temperature = forecast.temp_c;
                var realFeeling = forecast.feelslike_c;
                var humidity = forecast.humidity;
                var pressure = forecast.pressure_in;
                var city = queryLocation.name;
                var region = queryLocation.region;
                var country = queryLocation.country;
                $scope.aiResponseData = response.AiResponse;
                $scope.chat.unshift({
                    self: false,
                    msgs: [
                        city + ", " + region,
                        country,
                        "The temperature is " + temperature + "℃",
                        "But feels like " + realFeeling + "℃",
                        "Humidity is " + humidity + " and pressure is " + pressure,
                    ],
                    aiResponse: $scope.aiResponseData
                });
            }).error(function errorCallback(response, statusCode) {
                $scope.aiResponseData = response.AiResponse;
                if (statusCode === 400) {
                    $scope.chat.unshift({
                        self: false,
                        msgs: ["I'm sorry, I didn't get that, can you ask in some other way, please"],
                        aiResponse: $scope.aiResponseData
                    });
                }
                else if (statusCode === 404) {
                    $scope.chat.unshift({
                        self: false,
                        msgs: ["I'm sorry, I don't know that place, can you be more specific or ask about somewhere else"],
                        aiResponse: $scope.aiResponseData
                    });
                }
                else {
                    $scope.chat.unshift({
                        self: false,
                        msgs: ["There was an error, I asssure you we have the best code monkeys working to sort it out"],
                        aiResponse: $scope.aiResponseData
                    });
                }
            });
        $scope.comment = "";
    }

    $scope.showAIResponse = function (data) {
        if (data != "") {
            $scope.aiResponseData = data;
        }
    }
}];

angular.module("weatherAiDemo").controller('BaseAiServiceController', baseAiServiceController);