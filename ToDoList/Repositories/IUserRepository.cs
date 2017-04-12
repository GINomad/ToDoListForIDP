using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;
using ToDoList.ViewModels;

namespace ToDoList.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<UserViewModel> GetNonAssignedUsers(int taskId);
        List<UserStaristicViewModel> GetUserStatistic();
    }
}
