using System;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.ViewModels 
{
    public class TaskViewModel
    {
        public int TaskId { get; set; }

        public string Title { get; set; }

        public DateTime DueDate { get; set; }

        public Priority TaskPriority { get; set; }

        public float TimeEstimated { get; set; }

        public int GroupId { get; set;  }

        public bool Closed { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public int CommentCount { get; set; }

        public TaskViewModel()
        {
            Comments = new List<CommentViewModel>();
        }
        public TaskViewModel(string Title)
        {
            
            this.Title = Title;
            this.Closed = false;
            TaskPriority = Priority.None;
            DueDate = DateTime.Today;
        }
    }
}