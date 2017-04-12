using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.ViewModels
{
    public class TaskAssignmentModel
    {
        public int TaskId { get; set; }
        public string UserId { get; set; }

        public int GroupId { get; set; }
    }
}