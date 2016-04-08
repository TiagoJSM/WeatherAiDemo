var apiAiController = ["$scope", "$http", function ($scope, $http) {
    $scope.comment = "";
    $scope.chat = [];

    $scope.sendText = function () {
        $scope.chat.push({
            self: true,
            msgs: [$scope.comment]
        });
        
        $http.post("/api/apiai/ask", { question: $scope.comment })
            .success(function successCallback(response) {
                var forecast = response.ForecastResult.current;
                var temperature = forecast.temp_c;
                var realFeeling = forecast.feelslike_c;
                var humidity = forecast.humidity;
                var pressure = forecast.pressure_in;
                $scope.chat.push({
                    self: false,
                    msgs: [
                        "The temperature is " + temperature + "℃",
                        "But feels like " + realFeeling + "℃",
                        "Humidity is " + humidity + " and pressure is " + pressure,
                    ]
                });
            }).error(function errorCallback(response) {
                if(response){
                    $scope.chat.push({
                        self: false,
                        msgs: ["There was an error, I asssure you we have the best code monkeys working to sort it out"]
                    });
                }
            })
            .catch(function (err) {
                if (err.status === 400) {
                    $scope.chat.push({
                        self: false,
                        msgs: ["I'm sorry, I didn't get that, can you ask in some other way, please"]
                    });
                }
                else if(err.status === 404) {
                    $scope.chat.push({
                        self: false,
                        msgs: ["I'm sorry, I don't know that place, can you be more specific or ask about somewhere else"]
                    });
                }
            }); 
        $scope.comment = "";
    }
}];

angular.module("weatherAiDemo").controller('ApiAiController', apiAiController);