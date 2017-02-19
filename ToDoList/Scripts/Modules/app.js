var todoApp = angular.module('todoApp',[]);
todoApp.controller("taskController", function ($scope, $http) {
    /*Модели*/
    $scope.tasks = {};
    $scope.title;
    $scope.forEdit;
    $scope.currentTask = {};

    /* Методы CRUD-операций для тасок*/
    var getTasks = function ($scope, $http) {
        $http.get("task/tasks").then(function (responce) {
            $scope.tasks = responce.data;
            for(var i=0;i<responce.data.length;i++)
            {
                var date = $scope.tasks[i].DueDate;
                newdate = new Date(parseInt(date.substr(6)));
                $scope.tasks[i].DueDate = newdate;
            }
        });
    };
    $scope.newTask = function () {
        $http.post('task/add', { 'Title': $scope.title })
           .then(function ($scope) { location.reload(); });
    }

    $scope.selectTask = function (task) {
        $scope.forEdit = false;
        $scope.currentTask = task;
    }

    $scope.editTask = function (currentTask) {
        currentTask.isSelected = false;
        $scope.forEdit = true;
    }

    $scope.update = function (currentTask) {
        $http.post('task/edit', { 'model': currentTask }).then(function () { location.reload(); });
    }
    getTasks($scope,$http);
});