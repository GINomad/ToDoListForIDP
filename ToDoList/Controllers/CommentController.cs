using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentController()
        {
            _context = new ApplicationDbContext();
                }

        // GET: Comment
        [HttpPost]
        public ActionResult AddComment(CommentViewModel model)
        {
            Comment comm = new Comment();
            comm.CommentId = 0;
            comm.Text = model.Text;
            comm.TimePosted = DateTime.Now;
            comm.TaskId = model.TaskId;
            _context.Comments.Add(comm);
            _context.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    }
}