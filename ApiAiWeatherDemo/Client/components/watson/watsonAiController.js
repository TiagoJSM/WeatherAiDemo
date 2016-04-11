var watsonAiController = ["$scope", "$http", "$controller", function ($scope, $http, $controller) {
    $scope.askPath = "/api/watson/ask";
    $controller('BaseAiServiceController', { $scope: $scope });
}];

angular.module("weatherAiDemo").controller('WatsonAiController', watsonAiController);