using System.Collections.Generic;
using System.Linq;
using TodoApp.API.Abstraction.DataAccessLayers;
using TodoApp.API.Data;
using TodoApp.API.Models;

namespace TodoApp.API.DataAccessLayers.Implementation
{
    public class TodoDal : ITodoDal
    {
        private readonly DataContext _context;
        public TodoDal (DataContext context)
        {
            _context = context;
        }

        public Todo GetTodo(int id)
        {
            return _context.Todos.FirstOrDefault(t => t.Id == id);
        }

        public List<Todo> GetTodos(int folderId)
        {
            return _context.Todos.Where(t => t.FolderId == folderId && !t.IsDone).ToList();
        }

        public Todo AddTodo(Todo todo)
        {
            todo = _context.Todos.Add(todo).Entity;
            _context.SaveChanges();
            return todo;
        }

        public bool RemoveTodo(int id)
        {
            _context.Todos.Remove(GetTodo(id));
            _context.SaveChanges();
            return true;
        }

        public Todo TodoDone(int id)
        {
            Todo todo=GetTodo(id);
            todo.IsDone = true;
            _context.SaveChanges();
            return todo;
        }

        public void RemoveTodosByFolderId(int id)
        {
            List<Todo> todosToRemove = _context.Todos.Where(t => t.FolderId == id).ToList();
            foreach(Todo todo in todosToRemove)
                _context.Todos.Remove(todo);
            _context.SaveChanges();
        }

    }
}