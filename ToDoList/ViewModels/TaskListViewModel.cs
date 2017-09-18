using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.ViewModels
{
    public class TaskListViewModel
    {
        public IEnumerable<int> Ids { get; set; }

        public string Priority { get; set; }
    }
}