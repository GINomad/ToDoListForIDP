using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ToDoList.Models;
using ToDoList.Repositories;
using ToDoList.ViewModels;

namespace ToDoList.Controllers
{
    [System.Web.Http.Authorize]
    [System.Web.Http.RoutePrefix("api/comment")]
    public class CommentController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        // GET: Comment
        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("AddComment")]
        public IHttpActionResult AddComment([FromBody]CommentViewModel model)
        {
            _unitOfWork.Comments.AddComment(model);

            return Ok();
        }
    }
}