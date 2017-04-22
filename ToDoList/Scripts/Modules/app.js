var todoApp = angular.module('todoApp', ['ngRoute','LocalStorageModule']);

/*todoApp.config(function ($routeProvider) {
    $routeProvider.when('/', {
        templateUrl: 'home/tasks',
        controller: 'taskController'
    });

    $routeProvider.when('/login', {
        templateUrl: 'account/login',
        controller: "loginController"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "account/register"
    });

    $routeProvider.otherwise({ redirectTo: "/" });
});*/
todoApp.config(['$httpProvider',function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
    
}]);

todoApp.run(['authService', function (authService) {
    authService.fillAuthData();
}]);





todoApp.controller("statController", function ($rootScope, $scope, $http, $filter) {

    $scope.statisticInfo;
    $scope.getStatistic = function () {
        $http.get('/api/user/getStatistic').then(function (responce) {
            $scope.statisticInfo = responce.data;
        });
    }
    $scope.getStatistic();
});