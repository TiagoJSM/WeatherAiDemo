var apiAiController = ["$scope", "$http", "$controller", function ($scope, $http, $controller) {
    $scope.askPath = "/api/apiai/ask";
    $scope.apiName = "Api AI";
    $scope.apiHomePage = "http://www.api.ai";
    $scope.intentSRef = "apiaiIntentInfo";
    $controller('BaseAiServiceController', { $scope: $scope });
}];

angular.module("weatherAiDemo").controller('ApiAiController', apiAiController);