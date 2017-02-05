using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;
using ToDoList.Repositories;
using ToDoList.ViewModels;

namespace ToDoList.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Comment
        [HttpPost]
        public ActionResult AddComment(CommentViewModel model)
        {
            _unitOfWork.Comments.AddComment(model);

            return RedirectToAction("Index","Home");
        }
    }
}