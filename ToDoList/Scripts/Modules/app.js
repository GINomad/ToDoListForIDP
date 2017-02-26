var todoApp = angular.module('todoApp', []);

todoApp.controller("taskController", function ($scope, $http) {
    /*Модели*/
    $scope.tasks = {};
    $scope.title;
    $scope.forEdit;
    $scope.currentTask = {};
    $scope.showComment;
    $scope.newComment;
    $scope.isAllSelected;
    $scope.tab = 1;
    $scope.priority = ['None', 'High', 'Normal', 'Low'];
    /* Методы CRUD-операций для тасок*/
    var _getTasks = function (groupid) {
        $http.post("task/tasks", {'groupid': groupid}).then(function (responce) {
            $scope.tasks = responce.data;
            for(var i=0;i<responce.data.length;i++)
            {
                var date = $scope.tasks[i].DueDate;
                newdate = new Date(parseInt(date.substr(6)));
                $scope.tasks[i].DueDate = newdate;
                
            }
        });
    };
    $scope.getTasks = _getTasks;
    $scope.setTab = function (newTab) {
        $scope.tab = newTab;
        $scope.getTasks($scope.tab);
    };

    $scope.isSet = function (tabNum) {
        return $scope.tab === tabNum;
    };
    $scope.newTask = function () {
        $http.post('task/add', { 'Title': $scope.title })
           .then(function ($scope) { location.reload(); });
    }
    $scope.setColor = function (task)
    {
        if (task.isSelected) {
            switch (task.TaskPriority) {
                case 0: $('#color' + task.TaskId).addClass('priority-color-none'); break;
                case 1: $('#color' + task.TaskId).addClass('priority-color-high'); break;
                case 2: $('#color' + task.TaskId).addClass('priority-color-normal'); break;
                case 3: $('#color' + task.TaskId).addClass('priority-color-low'); break;

            }
        }
        else {
            switch (task.TaskPriority) {
                case 0: $('#color' + task.TaskId).removeClass('priority-color-none'); break;
                case 1: $('#color' + task.TaskId).removeClass('priority-color-high'); break;
                case 2: $('#color' + task.TaskId).removeClass('priority-color-normal'); break;
                case 3: $('#color' + task.TaskId).removeClass('priority-color-low'); break;

            }
        }
    }
    $scope.selectTask = function (task) {
        $scope.forEdit = false;
        $scope.currentTask = task;
        $scope.setColor($scope.currentTask);
        
    }

    $scope.editTask = function (currentTask) {
        currentTask.isSelected = false;
        $scope.forEdit = true;
    }

    $scope.update = function (currentTask) {
        $http.post('task/edit', { 'model': currentTask }).then(function () { location.reload(); });
    }
    $scope.addComment = function (currentTask, newComment) {
        var task = currentTask;
        var comment = {
            Text: newComment,
            TaskId: currentTask.TaskId
        };
        $http.post('comment/addComment', { 'model': comment }).then(function () { location.reload(); });
    }
    $scope.selectAll = function (isAllSelected) {
        if (isAllSelected)
        {
            for (var i = 0; i < $scope.tasks.length; i++) {
                $scope.tasks[i].isSelected = true;
                $scope.setColor($scope.tasks[i]);
            }
        }
        else {
            for (var i = 0; i < $scope.tasks.length; i++) {
                $scope.tasks[i].isSelected = false;
                $scope.setColor($scope.tasks[i]);
            }
        }              
    }
    $scope.setTab(1);
});