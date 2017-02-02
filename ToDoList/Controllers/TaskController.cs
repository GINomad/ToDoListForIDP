using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ToDoList.Models;
using ToDoList.ViewModels;

namespace ToDoList.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Task
        public ActionResult Tasks(int groupid = 1)
        {
            if (groupid == 1)
            {
                var task = _context.MyTasks.Where(t => t.Closed != true).ToList();
                return View("Tasks", task);
            }
            if (groupid > 1)
            {
                var closedGroupId = _context.Groups.
                    FirstOrDefault(x => x.GroupName == "Closed").
                    GroupId;
                var task = _context.MyTasks.Where(x => x.GroupId == groupid && x.Closed != true).ToList();

                if (groupid == closedGroupId)
                {
                    task = _context.MyTasks.Where(x => x.Closed == true).ToList();
                    return View("Tasks", task);
                }
                return View("Tasks", task);
            }
            else
            {
                var task = _context.MyTasks.Where(t => t.Closed != true).ToList();
                return View("Tasks", task);
            }
        }
        [HttpPost]
        public ActionResult Add(string Title)
        {
            if (Title == null)
            {
                return RedirectToAction("Index","Home");
            }
            TaskViewModel model = new TaskViewModel(Title);
            var task = Mapper.Map<TaskViewModel, MyTask>(model);
            _context.MyTasks.Add(task);
            _context.SaveChanges();
            return RedirectToAction("Index","Home");
        }
        public ActionResult Edit(string id, string act)
        {
            int ID = Convert.ToInt32(id);
            var task = _context.MyTasks.Where(t => t.MyTaskId == ID).Include(c => c.Comments).ToList();
            TaskViewModel model = new TaskViewModel();
            model = Mapper.Map<MyTask, TaskViewModel>(task.Single());

            if (!String.IsNullOrEmpty(act) && act == "edit")
            {
                return PartialView("EditPost", model);
            }

            return PartialView("Edit", model);
        }
        [HttpPost]
        public ActionResult Edit(TaskViewModel model)
        {
            int id = model.TaskId;
            var task = _context.MyTasks.FirstOrDefault(t => t.MyTaskId == id);
            if (task != null)
            {
                task.GroupId = model.GroupId;
                task.Closed = model.Closed;
                task.TaskPriority = model.TaskPriority;
                task.DueDate = model.DueDate;
                task.TimeEstimated = model.TimeEstimated;
                task.Title = model.Title;
                _context.SaveChanges();
            }
            return RedirectToAction("Index","Home");
        }
    }
}