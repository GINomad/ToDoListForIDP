using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ToDoList.Models;
using ToDoList.ViewModels;
using Microsoft.AspNet.Identity;

namespace ToDoList.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        private Dictionary<string, Priority> prior = new Dictionary<string, Priority>();
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
            prior.Add("None", Priority.None);
            prior.Add("High", Priority.High);
            prior.Add("Normal", Priority.Normal);
            prior.Add("Low", Priority.Low);
        }

        public IEnumerable<TaskViewModel> Tasks {
            get {
                return Mapper.Map<IEnumerable<MyTask>, IEnumerable<TaskViewModel>>(_context.MyTasks).ToList();
            }
        }

        public IEnumerable<TaskViewModel> GetClosedTasks()
        {
            return Tasks.Where(t => t.Closed == true);
        }
        public void AddTask(string Title, string id)
        {
            TaskViewModel model = new TaskViewModel(Title);
            model.ApplicationUserId = id;
            var task = Mapper.Map<TaskViewModel, MyTask>(model);
            _context.MyTasks.Add(task);
            _context.SaveChanges();
        }

        public TaskViewModel GetTaskById(int id)
        {
            var task = _context.MyTasks.Where(t => t.MyTaskId == id).Include(c => c.Comments).ToList();
            if(task == null)
            {
                throw new InvalidOperationException("I cannot found the user by id you sent");
            }
            TaskViewModel result = new TaskViewModel();
            result = Mapper.Map<MyTask, TaskViewModel>(task.Single());

            return result;
        }

        public void Update (TaskViewModel model)
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
        }

        public void Close (int id)
        {
            var task = GetTaskById(id);
            task.Closed = true;
            Update(task);
        }

        public void Delete(int id)
        {
            var task = _context.MyTasks.FirstOrDefault(t => t.MyTaskId == id);
            if(task != null)
            {
                _context.MyTasks.Remove(task);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Cannot delete task. Task not found");
            }
        }
        
        public void SetPriority(int id, string priority)
        {
            var task = _context.MyTasks.FirstOrDefault(t => t.MyTaskId == id); 
            if(task != null)
            {
                Priority temp;
                prior.TryGetValue(priority, out temp);
                task.TaskPriority = temp;
                _context.SaveChanges();
            }
        }
    }
}