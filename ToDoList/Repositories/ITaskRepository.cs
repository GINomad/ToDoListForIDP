using System.Collections.Generic;
using ToDoList.ViewModels;

namespace ToDoList.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<TaskViewModel> Tasks { get; }

        void AddTask(string Title);
        void Close(int id);
        void Delete(int id);
        TaskViewModel GetTaskById(int id);
        void Update(TaskViewModel model);
        IEnumerable<TaskViewModel> GetClosedTasks();
    }
}