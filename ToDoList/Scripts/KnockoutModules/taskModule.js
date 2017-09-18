'use strict'

var apiUrl = location.protocol + "//" + location.host + "/api";
    
var Task = function (id, title, dueDate, groupId, priority, isSelected) {
    var self = this;
    self.id = ko.observable(id);
    self.title = ko.observable(title);
    self.dueDate = ko.observable(dueDate);
    self.groupId = ko.observable(groupId);
    self.priority = ko.observable(priority);
    self.isSelected = ko.observable(isSelected);
    return self;
}

$(document).ready(function () {
    var taskVM = {
        tasks: ko.observableArray([]),
        setTask: function (task) {
            this.tasks.push(task);
        },
        onChange: function(task){
            alert(task.id())
        },
        isAllSelected: ko.observable(),
        selectAll: function () {
            var flag = this.isAllSelected();
            $.each(this.tasks(), function (index, item) {               
                item.isSelected(flag);
            })
        },
        
    }
    ko.applyBindings(taskVM);

    $.ajax({
        url: apiUrl + "/task/gettasks/1",
        method: "GET",
    }).success(function (response) {
        $.each(response, function (index, value) {
            taskVM.setTask(new Task(value.TaskId, value.Title, value.DueDate, value.GroupId, value.TaskPriority, value.isSelected));
        });
        
    });
    console.debug();
});