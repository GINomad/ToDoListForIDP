﻿using System;
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

        public string ApplicationUserId { get; set; }

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
        }
        public TaskViewModel(string Title)
        {
            
            this.Title = Title;
            this.Closed = false;
            TaskPriority = Priority.None;
            DueDate = DateTime.Today;
            _comments = new List<CommentViewModel>();
        }
    }
}