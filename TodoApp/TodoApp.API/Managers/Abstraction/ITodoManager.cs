using System.Collections.Generic;
using TodoApp.API.Models;

namespace TodoApp.API.Managers.Abstraction
{
    public interface ITodoManager
    {
        Todo AddTodo(string todoName, int folderId, int userId);
        bool RemoveTodo(int id, int userId);
        List<Todo> GetTodos(int folderId);
        Todo TodoDone(int todoId, int userId);
        void CreateInitialTodos(int folderId);
    }
}