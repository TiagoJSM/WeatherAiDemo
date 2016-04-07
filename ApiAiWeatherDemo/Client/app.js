(function () {
    var app = angular.module("weatherAiDemo", ["ngRoute", "ui.router"]);

    app.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state("index", {
                url: "/index",
                templateUrl: "Client/components/index/indexView.html"
            })
            .state("apiAi", {
                url: "/apiai",
                templateUrl: "Client/components/apiAi/apiAiView.html",
                controller: "ApiAiController"
            })
            .state("luis", {
                url: "/luis",
                templateUrl: "Client/components/luis/luisView.html",
                //controller: "EnterpriseSalesController"
            })
            .state("watson", {
                url: "/watson",
                templateUrl: "Client/components/watson/watsonView.html",
                //controller: "EnterpriseSalesController"
            });
        $urlRouterProvider
            .otherwise("/index");
    }]);

    app.run(['$rootScope', '$state', function ($rootScope, $state) {

        $rootScope.$on('$stateChangeStart', function (evt, to, params) {
            if (to.redirectTo) {
                evt.preventDefault();
                $state.go(to.redirectTo, params)
            }
        });
    }]);
}());