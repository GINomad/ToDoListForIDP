using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    public class TaskViewModel
    {
        public int TaskId { get; set; }
        public string Title { get; set; }

        public DateTime DueDate { get; set; }

        public Priority TaskPriority { get; set; }

        public float TimeEstimated { get; set; }

        public bool Closed { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public int CommentCount { get; set; }

    }
}