using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    //All models need to refactor
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpGet]
        public ActionResult GetGroups()
        {
            var groups = _context.Groups.ToList();
            return PartialView("GroupTabsPartial", groups);
        }
        public ActionResult Index(string groupid)
        {
            return View();
        }
        public ActionResult Tasks(string groupid)
        {
            if (!String.IsNullOrEmpty(groupid))
            {
                var closedGroupId = _context.Groups.
                    Where(x => x.GroupName == "Closed").
                    FirstOrDefault().
                    GroupId;
                int id = Convert.ToInt32(groupid);
                var task = _context.MyTasks.Where(x => x.GroupId == id && x.Closed != true).ToList();

                if (id == closedGroupId)
                {
                    task = _context.MyTasks.Where(x => x.Closed == true).ToList();
                    return View("Tasks",task);
                }
                return View("Tasks",task);
            }
            else
            {
                var task = _context.MyTasks.Where(t => t.Closed != true).ToList();
                return View("Tasks",task);
            }
        }
        [HttpPost]
        public ActionResult Add(string Title)
        {
            if (Title == null)
            {
                return RedirectToAction("Index");
            }
            MyTask task = new MyTask();
            task.Title = Title;
            task.TaskPriority = Priority.None;
            task.Closed = false;
            task.TimeEstimated = 0;
            task.DueDate = DateTime.Today;
            task.Closed = false;
            _context.MyTasks.Add(task);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string id, string act)
        {
            int ID = Convert.ToInt32(id);
            var task = _context.MyTasks.FirstOrDefault(x => x.MyTaskId == ID);
            TaskViewModel model = new TaskViewModel();
            model.TaskId = task.MyTaskId;
            model.TaskPriority = task.TaskPriority;
            model.Title = task.Title;
            model.DueDate = task.DueDate;
            model.CommentCount = _context.Comments.Count(c => c.TaskId == task.MyTaskId);
            var Comments = _context.Comments.Where(c => c.TaskId == task.MyTaskId).ToList();
            List<CommentViewModel> comm = new List<CommentViewModel>();
            foreach (var item in Comments)
            {
                var temp = new CommentViewModel();
                temp.ID = item.CommentId;
                temp.Text = item.Text;
                temp.TaskId = item.TaskId;
                comm.Add(temp);
            }
            model.Comments = comm;
            if (!String.IsNullOrEmpty(act) && act=="edit")
            {
                return PartialView("EditPost", model);
            }
            
            return PartialView("Edit",model);
        }
        [HttpPost]
        public ActionResult Edit(TaskViewModel model)
        {
            int id = model.TaskId;
            var task = _context.MyTasks.FirstOrDefault(t => t.MyTaskId == id);
            if(task != null)
            {
                task.TaskPriority = model.TaskPriority;
                task.Title = model.Title;
                task.DueDate = model.DueDate;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }



    }
}