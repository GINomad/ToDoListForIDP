using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using ToDoList.Models;
using ToDoList.Repositories;
using ToDoList.ViewModels;

namespace ToDoList.Controllers
{
    [System.Web.Http.RoutePrefix("api/task")]
    public class TaskApiController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/task/gettasks/{groupid}")]
        public IEnumerable<TaskViewModel> Get()
        {
            var model = _unitOfWork.Tasks.Tasks.ToList();
            return model;
        }
    }
}
