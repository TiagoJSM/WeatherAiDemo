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
                  url: "/apiai/intents",
                  templateUrl: "Client/components/apiAi/intentInfoView.html",
                  controller: "ApiAiIntentController"
              })
            .state("apiaiUpdateIntentInfo", {
                url: "/apiai/intents/update",
                templateUrl: "Client/components/apiAi/apiAiUpdateIntentView.html",
                controller: "UpdateApiAiIntentController"
            })
            .state("luis", {
                url: "/luis",
                templateUrl: "Client/components/luis/luisView.html",
                controller: "LuisAiController"
            })
            .state("luisIntentInfo", {
                url: "/luis/intents",
                templateUrl: "Client/components/luis/intentInfoView.html",
                controller: "LuisIntentController"
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