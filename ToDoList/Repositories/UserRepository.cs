using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ToDoList.Models;
using ToDoList.ViewModels;

namespace ToDoList.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<UserViewModel> GetNonAssignedUsers(int taskId)
        {
            var userList = _context.Users.ToList();
            var task = _context.MyTasks.FirstOrDefault(t => t.MyTaskId == taskId);
            _context.Entry(task).Collection(u => u.Users).Load();

            if(task != null)
            {
                
                
                foreach (var user in task.Users)
                {
                    if (userList.Contains(user))
                    {
                        userList.Remove(user);
                    }
                }
                return userList.Select(x => new UserViewModel{Id = x.Id, UserName = x.UserName });
            }
            else
            {
                return null;
            }
            
        }

        public List<UserStaristicViewModel> GetUserStatistic()
        {
            var users = _context.Users.Include(t => t.Tasks).ToList();

            var result = new List<UserStaristicViewModel>();
            foreach(var user in users)
            {
                UserStaristicViewModel model = new UserStaristicViewModel();
                model.UserName = user.UserName;
                model.TaskCount = user.Tasks.Count;

                result.Add(model);
            }
            return result;
        }
    }
}