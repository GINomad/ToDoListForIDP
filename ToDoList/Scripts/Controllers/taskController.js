todoApp.controller("taskController", function ($scope,$http) {
    var getTasks = function ($scope, $http) {
        $http.get("api/get").then(function (responce) {
            $scope.tasks = responce.data;
        });
    };
    getTasks();
    $scope.tak = {
        name: 'Test'
    }
});