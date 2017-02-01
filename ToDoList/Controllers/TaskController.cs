using System;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using ToDoList.Models;
using ToDoList.ViewModels;

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
        public IHttpActionResult Add(string Title)
        {
            if (Title == null)
            {
                return BadRequest();
            }
           TaskViewModel model = new TaskViewModel(Title);
           var task =  Mapper.Map<TaskViewModel, MyTask>(model);
            _context.MyTasks.Add(task);
            _context.SaveChanges();

            return Ok();
        }
    }
}
