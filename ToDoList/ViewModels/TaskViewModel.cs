using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Models;

namespace ToDoList.ViewModels 
{
    public class TaskViewModel
    {
        private List<CommentViewModel> _comments;
        public int TaskId { get; set; }

        public string Title { get; set; }

        public DateTime DueDate { get; set; }

        public Priority TaskPriority { get; set; }

        public float TimeEstimated { get; set; }

        public int GroupId { get; set;  }

        public bool Closed { get; set; }

        public bool isSelected { get; set; }

        // public string ApplicationUserId { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
        public IEnumerable<CommentViewModel> Comments
        {
            get { return _comments; }
            set
            {
                _comments = value.ToList();
                CommentCount = Comments.Count();
            } 
        }

        public int CommentCount { get; set; }

        public TaskViewModel()
        {
            _comments = new List<CommentViewModel>();
            Users = new List<UserViewModel>();
        }
        public TaskViewModel(string Title)
        {
            
            this.Title = Title;
            this.Closed = false;
            this.GroupId = 1;
            TaskPriority = Priority.None;
            DueDate = DateTime.Today;
            _comments = new List<CommentViewModel>();
        }
    }
}