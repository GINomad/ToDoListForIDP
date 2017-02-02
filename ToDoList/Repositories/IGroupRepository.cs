using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.Repositories
{
    public interface IGroupRepository
    {
        Group GetGroupById(int id);
        Group GetGroupByName(string name);
        int GetGroupId(string name);
        IEnumerable<Group> Groups { get; }
    }
}