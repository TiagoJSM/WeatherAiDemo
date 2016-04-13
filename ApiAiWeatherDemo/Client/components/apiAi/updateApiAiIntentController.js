var updateApiAiIntentController = ["$scope", "$http", function ($scope, $http) {
    $scope.intentInfo = ""
    $scope.intentStatus = ""

    $http.get("/api/apiai/intents")
    .then(function successCallback(response) {
        $scope.intentInfo = JSON.stringify(response.data, undefined, 4)
       
    }, function errorCallback(response) {
        // called asynchronously if an error occurs
        // or server returns response with an error status.
    });

    $scope.updateIntent = function(){
        $http.put("/api/apiai/intents/update", $scope.intentInfo)
            .then(function successCallback(response) {
                $scope.intentStatus = response.data
            }, function errorCallback(response) {
                $scope.intentStatus = response.data
                // called asynchronously if an error occurs
                // or server returns response with an error status.
            });
    }
    
}];

angular.module("weatherAiDemo").controller('UpdateApiAiIntentController', updateApiAiIntentController);