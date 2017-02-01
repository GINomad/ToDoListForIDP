using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using System.Collections;
using System.Collections.Generic;

namespace ToDoList.Models
{
    public class MyTask
    {
        public int MyTaskId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public float TimeEstimated { get; set; }

        public DateTime DueDate { get; set; }

        public Priority TaskPriority { get; set; }

        public virtual Group Group { get; set; }

        public int? GroupId { get; set; }

        public bool Closed { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

        public MyTask()
        {
            Comments = new List<Comment>();
        }
    }
}