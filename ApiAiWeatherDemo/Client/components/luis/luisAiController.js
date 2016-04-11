var luisAiController = ["$scope", "$http", "$controller", function ($scope, $http, $controller) {
    $scope.askPath = "/api/luis/ask";
    $controller('BaseAiServiceController', { $scope: $scope });
}];

angular.module("weatherAiDemo").controller('LuisAiController', luisAiController);