var apiAiController = ["$scope", "$http", function ($scope, $http) {
    $scope.comment = "";
    $scope.chat = [];
    $scope.aiResponse = "";
    $scope.responseArea = "";
    $scope.responseData = "";
    $scope.response = "";

    $scope.sendText = function () {
        $scope.chat.push({
            self: true,
            msgs: [$scope.comment]
        });
        
        $http.post("/api/apiai/ask", { question: $scope.comment })
            .then(function successCallback(response) {
                var forecast = response.data.ForecastResult.current;
                var temperature = forecast.temp_c;
                var realFeeling = forecast.feelslike_c;
                var humidity = forecast.humidity;
                var pressure = forecast.pressure_in;
                $scope.aiResponse = response.data.ApiAIResponse;
                $scope.chat.push({
                    self: false,
                    msgs: [
                        "The temperature is " + temperature + "℃",
                        "But feels like " + realFeeling + "℃",
                        "Humidity is " + humidity + " and pressure is " + pressure,
                    ]
                });
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
            });
        $scope.comment = "";
    }

    $scope.getResponseInfo = function (element) {
        var aiData = $scope.aiResponse;
        var strigify = JSON.stringify(aiData, null, "    ");
        if (angular.element(element).hasClass('other')) {
            $scope.responseData = strigify;
        }
    }
}];

angular.module("weatherAiDemo").controller('ApiAiController', apiAiController);