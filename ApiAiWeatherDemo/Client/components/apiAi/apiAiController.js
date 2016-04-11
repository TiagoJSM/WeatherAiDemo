var apiAiController = ["$scope", "$http", "$controller", function ($scope, $http, $controller) {
    $scope.askPath = "/api/apiai/ask";
    $controller('BaseAiServiceController', { $scope: $scope });
}];

angular.module("weatherAiDemo").controller('ApiAiController', apiAiController);