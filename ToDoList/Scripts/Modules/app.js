var todoApp = angular.module('todoApp', ['ngRoute','LocalStorageModule']);


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