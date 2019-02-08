using System.Collections.Generic;
using TodoApp.API.Abstraction.DataAccessLayers;
using TodoApp.API.Models;
using TodoApp.API.Managers.Abstraction;

namespace TodoApp.API.Managers.Implementation
{
    public class TodoManager: ITodoManager
    {
        private readonly ITodoDal _todoDal;
        private readonly IFolderDal _folderDal;
        public TodoManager(ITodoDal todoDal, IFolderDal folderDal)
        {
            _todoDal = todoDal;
            _folderDal = folderDal;
        }
        
        public List<Todo> GetTodos(int folderId)
        {
            return _todoDal.GetTodos(folderId);
        }

        public Todo AddTodo(string todoName, int folderId, int userId) 
        {
            if(_folderDal.GetFolder(folderId).UserId != userId)
            {
                return null;
            }

            Todo todo=new Todo(todoName, folderId);
            return _todoDal.AddTodo(todo);
        }

        public bool RemoveTodo(int id, int userId)
        {
            Todo todo = _todoDal.GetTodo(id);

            if(_folderDal.GetFolder(todo.FolderId).UserId != userId) {
                return false;
            }

            return _todoDal.RemoveTodo(id);
        }

        public Todo TodoDone(int todoId, int userId)
        {
            Todo todo = _todoDal.GetTodo(todoId);
            if(todo == null)
                return null;

            if(_folderDal.GetFolder(todo.FolderId).UserId != userId)
                return null;
                
            return _todoDal.TodoDone(todoId);
        }

        public void CreateInitialTodos(int folderId)
        {
            _todoDal.AddTodo(new Todo("Your first to-do", folderId));
            _todoDal.AddTodo(new Todo("Your second to-do", folderId));
            _todoDal.AddTodo(new Todo("Your third to-do", folderId));
        }
    }
}