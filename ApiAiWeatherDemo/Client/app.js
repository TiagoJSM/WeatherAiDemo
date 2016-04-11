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
              .state("apiaiIntentInfo", {
                  url: "/apiai/intentInfo",
                  templateUrl: "Client/components/apiAi/intentInfoView.html",
                  controller: "ApiAiIntentController"
              })
            .state("luis", {
                url: "/luis",
                templateUrl: "Client/components/luis/luisView.html",
                controller: "LuisAiController"
            })
            .state("watson", {
                url: "/watson",
                templateUrl: "Client/components/watson/watsonView.html",
                controller: "WatsonAiController"
            })
            .state("about", {
                url: "/about",
                templateUrl: "Client/components/about.html",
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