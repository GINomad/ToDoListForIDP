using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int MyTaskId { get; set; }

        public MyTask Task { get; set; }

        public DateTime TimePosted { get; set; }
    }
}