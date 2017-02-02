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

namespace ToDoList.Controllers
{
    public class TaskController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Task
        public ActionResult Tasks(int groupid)
        {
            if (groupid == 1)
            {
                var task = _unitOfWork.Tasks.
                    Tasks.
                    Where(t => t.Closed != true)
                    .ToList();
                return View("Tasks", task);
            }
            if (groupid > 1)
            {
                var closedGroupId = _unitOfWork.Groups.GetGroupId("Closed");
                var task = _unitOfWork.Tasks.Tasks.Where(t => t.Closed != true && t.GroupId ==groupid);

                if (groupid == closedGroupId)
                {
                    task = _unitOfWork.Tasks.GetClosedTasks();
                    return View("Tasks", task);
                }
                return View("Tasks", task);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }          
        }
        [HttpPost]
        public ActionResult Add(string Title)
        {
            if (Title == null)
            {
                return RedirectToAction("Index","Home");
            }

            _unitOfWork.Tasks.AddTask(Title);

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
    }
}