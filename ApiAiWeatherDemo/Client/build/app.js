!function(){var app=angular.module("weatherAiDemo",["ui.router"]);app.config(function($stateProvider,$urlRouterProvider){$stateProvider.state("menubar",{url:"",templateUrl:"Client/menubar.html",redirectTo:"menubar.index"}).state("menubar.index",{url:"/index",templateUrl:"Client/controllers/index/index.html"}),$urlRouterProvider.otherwise("/index")}),app.run(["$rootScope","$state",function($rootScope,$state){$rootScope.$on("$stateChangeStart",function(evt,to,params){to.redirectTo&&(evt.preventDefault(),$state.go(to.redirectTo,params))})}])}();
//# sourceMappingURL=app.js.map