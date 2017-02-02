using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Models;
using ToDoList.ViewModels;

namespace ToDoList.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository( ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddComment(CommentViewModel model)
        {
            Comment comm = new Comment();
            comm.CommentId = 0;
            comm.Text = model.Text;
            comm.TimePosted = DateTime.Now;
            comm.MyTaskId = model.TaskId;
            _context.Comments.Add(comm);
            _context.SaveChanges();
        }
    }
}