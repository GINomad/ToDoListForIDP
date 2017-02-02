using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Models;

namespace ToDoList.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _context;
        public GroupRepository( ApplicationDbContext  context)
        {
            _context = context;
        }

        public IEnumerable<Group> Groups {
            get { return _context.Groups; }
        }

        public Group GetGroupByName(string name)
        {
            var group = _context.Groups.
                Where(g => g.GroupName.ToLower() == name.ToLower())
                .FirstOrDefault();

            if (group != null)
            {
                return group;
            }
            else
            {
                return null;
            }
        }

        public int GetGroupId(string name)
        {
            var group = this.GetGroupByName(name);
            if(group != null)
            {
                return group.GroupId;
            }
            else
            {
                return 0;
            }             
        }
        public Group GetGroupById(int id)
        {
            var group = _context.Groups.FirstOrDefault(g => g.GroupId == id);
            if(group != null)
            {
                return group;
            }
            else
            {
                return null;
            }
        }
    }
}