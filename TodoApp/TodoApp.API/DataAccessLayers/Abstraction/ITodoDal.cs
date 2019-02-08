using System;
using System.Collections.Generic;
using TodoApp.API.Models;

namespace TodoApp.API.Abstraction.DataAccessLayers
{
    public interface ITodoDal
    {
        Todo GetTodo(int id);
        List<Todo> GetTodos(int folderId);
        Todo AddTodo(Todo todo);
        bool RemoveTodo(int id);
        Todo TodoDone(int id);
        void RemoveTodosByFolderId(int id);
    }
}