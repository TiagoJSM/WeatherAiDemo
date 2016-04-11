﻿var luisAiController = ["$scope", "$http", function ($scope, $http) {
    $scope.comment = "";
    $scope.chat = [];
    $scope.aiResponseData = "";

    $scope.sendText = function () {
        $scope.chat.unshift({
            self: true,
            msgs: [$scope.comment],
            aiResponse: ""
        });

        $http.post("/api/luis/ask", { question: $scope.comment })
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
                $scope.aiResponseData = response.LuisResponse;
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
                if (statusCode === 400) {
                    $scope.chat.unshift({
                        self: false,
                        msgs: ["I'm sorry, I didn't get that, can you ask in some other way, please"]
                    });
                }
                else if (statusCode === 404) {
                    $scope.chat.unshift({
                        self: false,
                        msgs: ["I'm sorry, I don't know that place, can you be more specific or ask about somewhere else"]
                    });
                }
                else {
                    $scope.chat.unshift({
                        self: false,
                        msgs: ["There was an error, I asssure you we have the best code monkeys working to sort it out"]
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

angular.module("weatherAiDemo").controller('LuisAiController', luisAiController);