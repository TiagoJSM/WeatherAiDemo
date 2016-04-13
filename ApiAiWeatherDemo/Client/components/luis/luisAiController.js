var luisAiController = ["$scope", "$http", "$controller", function ($scope, $http, $controller) {
    $scope.askPath = "/api/luis/ask";
    $scope.apiName = "Luis";
    $scope.apiHomePage = "http://www.luis.ai";
    $scope.intentSRef = "luisIntentInfo";
    $controller('BaseAiServiceController', { $scope: $scope });
}];

angular.module("weatherAiDemo").controller('LuisAiController', luisAiController);