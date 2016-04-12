var luisIntentController = ["$scope", "$http", function ($scope, $http) {

    $http.get("/api/luis/intents")
        .then(function successCallback(response) {
            $scope.intentInfo = response.data
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
}];

angular.module("weatherAiDemo").controller('LuisIntentController', luisIntentController);