namespace ToDoList.Repositories
{
    public interface IUnitOfWork
    {
        ICommentRepository Comments { get; }

        ITaskRepository Tasks { get; }

        IGroupRepository Groups { get; }
    }
}