var watsonAiController = ["$scope", "$http", "$controller", function ($scope, $http, $controller) {
    $scope.askPath = "/api/watson/ask";
    $scope.apiName = "Watson";
    $scope.apiHomePage = "http://www.ibm.com/smarterplanet/us/en/ibmwatson";
    $scope.intentSRef = "watsonSchema";
    $controller('BaseAiServiceController', { $scope: $scope });
}];

angular.module("weatherAiDemo").controller('WatsonAiController', watsonAiController);