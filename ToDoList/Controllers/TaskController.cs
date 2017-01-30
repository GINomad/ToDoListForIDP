using System;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class TaskDto
    {
        public string Title { get; set; }
    }

    public class TaskController : ApiController
    {
        public readonly ApplicationDbContext _context;
        public TaskController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Add([FromBody]string Title)
        {
            if (Title == null)
            {
                return BadRequest();
            }
            var userId = User.Identity.GetUserId();
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            MyTask task = new MyTask();
            task.Title = Title;
            task.TaskPriority = Priority.None;
            task.Closed = false;
            task.TimeEstimated = 0;
            task.DueDate = DateTime.Today;
            task.Closed = false;
            _context.MyTasks.Add(task);
            _context.SaveChanges();

            return Ok();
        }
    }
}
