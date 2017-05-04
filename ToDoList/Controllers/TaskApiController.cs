using Microsoft.AspNet.Identity;
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
    [System.Web.Http.Authorize]
    [System.Web.Http.RoutePrefix("api/task")]
    public class TaskApiController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskApiController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
            //_unitOfWork = unitOfWork;
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("gettasks/{groupid}")]
        public IEnumerable<TaskViewModel> Get(int groupid)
        {
            var id = RequestContext.Principal.Identity.GetUserId();
            if (groupid == 4)
            {
                return _unitOfWork.Tasks.GetClosedTasks().Where(t => t.Users.Any(x => x.Id == id)).ToList();
            }
            else
            {
                if (groupid == 1)
                {
                    return _unitOfWork.Tasks.Tasks.Where(t => t.Closed != true && t.Users.Any(x => x.Id == id)).ToList();
                }
                else
                {
                    return _unitOfWork.Tasks.Tasks.Where(t => t.GroupId == groupid && t.Closed != true && t.Users.Any(x => x.Id == id)).ToList();
                }

            }          
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("Add")]
        public IHttpActionResult NewTask(string title)
        {
            if (title != null)
            {
                var id = RequestContext.Principal.Identity.GetUserId();
                _unitOfWork.Tasks.AddTask(title, id);
                return Ok();
            }
            else
            {
                return BadRequest("Unnable to add a task with empty title");
            }           
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("Edit")]
        public IHttpActionResult Edit([FromBody] TaskViewModel model)
        {
            _unitOfWork.Tasks.Update(model);
            return Ok();
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("Delete")]
        public IHttpActionResult Delete([FromBody] TaskListViewModel model)
        {
            if (model.Ids != null)
            {
                foreach(var id in model.Ids)
                {
                    if(id != 0)
                    {
                        _unitOfWork.Tasks.Delete(id);
                    }
                }
                
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("ChangePriority")]
        public IHttpActionResult ChangePriority([FromBody] TaskListViewModel model)
        {
            if (model.Ids != null)
            {
                foreach (var id in model.Ids)
                {
                    if (id != 0)
                    {
                        _unitOfWork.Tasks.SetPriority(id,model.Priority);
                    }
                }

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("Complete")]
        public IHttpActionResult Complete([FromBody] TaskListViewModel model)
        {
            if (model.Ids != null)
            {
                foreach (var id in model.Ids)
                {
                    if (id != 0)
                    {
                        _unitOfWork.Tasks.Close(id);
                    }
                }

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("Assign")]
        public IHttpActionResult AssignUser([FromBody] TaskAssignmentModel model)
        {
            _unitOfWork.Tasks.Assign(model.UserId, model.TaskId);
            return Ok();
        }
    }
}
