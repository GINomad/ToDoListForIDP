using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ToDoList.Models;
using ToDoList.ViewModels;
using ToDoList.Repositories;
using Microsoft.AspNet.Identity;

namespace ToDoList.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Task
        [HttpPost]
        public JsonResult Tasks(int groupid)
        {
            var userid = User.Identity.GetUserId();
            if (groupid == 4)
            {
                return  Json(_unitOfWork.Tasks.GetClosedTasks().Where(t => t.Users.Any(x=> x.Id == userid)), JsonRequestBehavior.AllowGet);
            }            
            else
            {
                if(groupid == 1)
                {
                    return Json(_unitOfWork.Tasks.Tasks.Where(t => t.Closed != true && t.Users.Any(x => x.Id == userid)), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(_unitOfWork.Tasks.Tasks.Where(t => t.GroupId == groupid && t.Closed != true && t.Users.Any(x => x.Id == userid)), JsonRequestBehavior.AllowGet);
                }
                
            }
            
        }

        [HttpPost]
        public ActionResult Add(string Title)
        {
            if (Title == null)
            {
                return RedirectToAction("Index","Home");
            }
            var id = User.Identity.GetUserId();
            _unitOfWork.Tasks.AddTask(Title, id);

            return RedirectToAction("Index","Home");
        }
        public ActionResult Edit(string id, string act)
        {
            int ID = Convert.ToInt32(id);
            var model = _unitOfWork.Tasks.GetTaskById(ID);

            if (!String.IsNullOrEmpty(act) && act == "edit")
            {
                return PartialView("EditPost", model);
            }

            return PartialView("Edit", model);
        }
        [HttpPost]
        public ActionResult Edit(TaskViewModel model)
        {
            if (model != null)
            {
                _unitOfWork.Tasks.Update(model);
            }
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public ActionResult DeleteAll(int [] id)
        {
            if(id != null)
            {
                for (int i = 0; i < id.Length; i++)
                {
                    if(id[i] != 0)
                    _unitOfWork.Tasks.Delete(id[i]);
                }
                Session["Response"] = "";
                return RedirectToAction("Index","Home",new {id=1 });
            }
            else
            {
                Session["Response"] = "You must choice one or more tasks before deleting";
                return RedirectToAction("Index", "Home", new { id=1});

            }
        } 
        [HttpPost]
        public ActionResult SetPriorityAll(int [] id, string priority)
        {
            for(int i = 0;i<id.Length;i++)
            {
                if(id[i] != 0)
                _unitOfWork.Tasks.SetPriority(id[i], priority);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Complete (int [] id)
        {
            for(int i=0;i<id.Length;i++)
            {
                if(id[i] != 0)
                {
                    _unitOfWork.Tasks.Close(id[i]);
                }
            }
            return RedirectToAction("Index","Home", new {groupid = 4 });
        }
    }
}