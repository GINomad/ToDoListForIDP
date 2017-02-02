using ToDoList.ViewModels;

namespace ToDoList.Repositories
{
    public interface ICommentRepository
    {
        void AddComment(CommentViewModel model);
    }
}