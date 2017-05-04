todoApp.config(function ($routeProvider) {
    $routeProvider.when('/home', {
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

    $routeProvider.when("/statistic", {
        controller: "statController",
        templateUrl: "home/statistic"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });
});

