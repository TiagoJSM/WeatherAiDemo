var watsonSchemaController = ["$scope", "$http", function ($scope, $http) {

    $http.get("/api/watson/schema")
        .then(function successCallback(response) {
            $scope.watsonSchema = response.data
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
}];

angular.module("weatherAiDemo").controller('WatsonSchemaController', watsonSchemaController);