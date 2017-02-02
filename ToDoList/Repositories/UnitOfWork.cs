using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Models;

namespace ToDoList.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ITaskRepository Tasks { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public IGroupRepository Groups { get; private set; }
        
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Tasks = new TaskRepository(context);
            Comments = new CommentRepository(context);
            Groups = new GroupRepository(context);
        } 
    }
}