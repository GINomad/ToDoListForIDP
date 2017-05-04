'use strict'
todoApp.controller("taskController",function ($rootScope, $scope, $http, $filter) {
    /*Модели*/
    var serviceBase = 'http://localhost:3007/';
    $scope.tasks = {};
    $scope.title = "";
    $scope.forEdit;
    $scope.currentTask = {};
    $scope.showComment;
    $scope.newComment;
    $scope.isAllSelected;
    $scope.tab = 1;
    $scope.detailsTab = 1;
    $scope.date;
    $scope.priority = ['None', 'High', 'Normal', 'Low'];
    $scope.users = [];
    $scope.showSelect = false;
    $scope.selected = [];
    $scope.currentTaskId;
    $scope.statisticInfo = [];


    /* Методы CRUD-операций для тасок*/
    var _getTasks = function (groupid) {       
        $http.get(serviceBase + "api/task/gettasks/" + groupid).then(function (responce) {
            $scope.tasks = responce.data;
            for(var i=0;i<responce.data.length;i++)
            {
                var string = $scope.tasks[i].DueDate.toString().replace(/\/Date\((-?\d+)\)\//, '$1');
                $scope.tasks[i].DueDate = $filter('date')(string, 'dd/MM/yyyy');               
            }
        });
    };

    var _getUsers = function (taskId) {
        if(taskId != $scope.currentTaskId)
        {
            $scope.showSelect = false;
        }
        $scope.currentTaskId = taskId;
        $scope.showSelect = !$scope.showSelect;
        $http.get(serviceBase + "api/user/getUsers/"+taskId).then(function (responce) {
            $scope.users = responce.data;
        })
    }
    $scope.getUsers = _getUsers;
    $scope.getTasks = _getTasks;
   
    $scope.setTab = function (newTab) {
        $scope.tab = newTab;
        $scope.currentTask.isSelected = false;
        $scope.getTasks($scope.tab);
    };

    $scope.setDetailsTab = function (newTab)
    {
        $scope.detailsTab = newTab;

    }

    $scope.isSet = function (tabNum) {
        return $scope.tab === tabNum;
    };

    $scope.isDetailsSet = function(tabNum)
    {
        return $scope.detailsTab === tabNum;
    }

    $scope.newTask = function () {
        if ($scope.title != "")
        {
            $http.post(serviceBase + 'api/taskapi/add?title='+ $scope.title)
                      .then(function ($scope) { location.reload(); });
        }     
    }
    $scope.setGroup = function(groupid)
    {
        $scope.currentTask.GroupId = groupid;
    }

    $scope.assign = function (index)
    {
        $http({
            method: 'POST',
            url: serviceBase + 'api/taskapi/assign',
            data: JSON.stringify({ userId: $scope.tasks[index].assignedUser, taskId: $scope.tasks[index].TaskId })
        });           
        $scope.tasks[index].assignedUser = "";
        $scope.tasks[index].HasAssignedUsers = false;
        $scope.showSelect = !$scope.showSelect;

    }

    $scope.changeAssignment = function(index,user)
    {
        $scope.tasks[index].HasAssignedUsers = true;
        $scope.tasks[index].assignedUser = user.Id;
    }

    $scope.complete = function () {
        var selectedTasks = [];
        angular.forEach($scope.tasks, function (value, key) {
            if(value.isSelected)
            {
                selectedTasks.push(value.TaskId);
            }
        });
        $http({
            method: 'POST',
            url: serviceBase + 'api/taskapi/complete',
            data: JSON.stringify({ 'ids': selectedTasks})
        }).then(function () { location.reload(); });
    }
    $scope.setColor = function (task)
    {
        if (task.isSelected) {
            $('#rowC' + task.TaskId).addClass('row-selected');
            $('#rowT' + task.TaskId).addClass('row-selected');
            $('#rowD' + task.TaskId).addClass('row-selected');
            switch (task.TaskPriority) {
                case 0: $('#color' + task.TaskId).addClass('priority-color-none');  break;
                case 1: $('#color' + task.TaskId).addClass('priority-color-high'); break;
                case 2: $('#color' + task.TaskId).addClass('priority-color-normal'); break;
                case 3: $('#color' + task.TaskId).addClass('priority-color-low'); break;

            }
        }
        else {
            $('#rowC' + task.TaskId).removeClass('row-selected');
            $('#rowT' + task.TaskId).removeClass('row-selected');
            $('#rowD' + task.TaskId).removeClass('row-selected');
            switch (task.TaskPriority) {
                case 0: $('#color' + task.TaskId).removeClass('priority-color-none'); break;
                case 1: $('#color' + task.TaskId).removeClass('priority-color-high'); break;
                case 2: $('#color' + task.TaskId).removeClass('priority-color-normal'); break;
                case 3: $('#color' + task.TaskId).removeClass('priority-color-low'); break;

            }
        }
    }
    $scope.setPriority = function(priority){
        var selectedTasks = [];
        angular.forEach($scope.tasks, function (value, key) {
            if(value.isSelected)
            {
                selectedTasks.push(value.TaskId);
            }
        });
        $http({
            method: 'POST',
            url: serviceBase + 'api/taskapi/changePriority',
            data: JSON.stringify({'ids':selectedTasks, 'priority': priority})
        }).then(function () { location.reload() });
    }
    $scope.selectDueToday = function () {
        for( var i = 0; i<$scope.tasks.length;i++)
        {
            var curentDate = moment().format('DD/MM/YYYY');
            var dueDate = $filter('date')($scope.tasks[i].DueDate, 'dd/MM/yyyy');
            if(dueDate == curentDate)
            {
                $scope.tasks[i].isSelected = true;
                $scope.setColor($scope.tasks[i]);
            }
        }
    }

    $scope.deleteAll = function ()
    {
        var selectedTasks = [];
        angular.forEach($scope.tasks, function (value, key) {
            if (value.isSelected) {
                selectedTasks.push(value.TaskId);
            }
        });
        if (selectedTasks.length != 0) {
            $http({
                method: 'POST',
                url: serviceBase + 'api/taskapi/delete',
                data: JSON.stringify({ 'ids': selectedTasks })
            }).then(function () { location.reload(); });
        }            
    }

    $scope.selectTask = function (task) {
        $scope.forEdit = false;
        $scope.currentTask = task;
        
        $scope.setColor($scope.currentTask);
        
    }

    $scope.update = function (currentTask) {
        $http({
            method: "POST",
            url: serviceBase + 'api/taskapi/edit',
            data: JSON.stringify(currentTask)
        }).then(function () { location.reload();});
    }
    $scope.addComment = function (currentTask, newComment) {
        var task = currentTask;
        var comment = {
            text: newComment,
            taskId: currentTask.TaskId
        };
        $http({
            method: 'POST',
            url: serviceBase + 'api/comment/addComment',
            data: JSON.stringify({ 'taskId': comment.taskId, 'text': comment.text })
        }).then(function () { location.reload(); });
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