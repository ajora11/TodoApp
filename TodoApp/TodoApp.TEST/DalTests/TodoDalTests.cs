using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using TodoApp.API.Abstraction.DataAccessLayers;
using TodoApp.API.Data;
using TodoApp.API.DataAccessLayers.Implementation;
using TodoApp.API.Managers.Abstraction;
using TodoApp.API.Managers.Implementation;
using TodoApp.API.Models;
using Xunit;

namespace TodoApp.TEST.DalTests
{
    public class TodoDalTests
    {
        private DataContext context;
        private TodoDal todoDal;

        public void GenerateNewFakeDatabase(string databaseName){
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;

            context = new DataContext(options);
            todoDal = new TodoDal(context);
        }
        
        [Fact]
        public void get_todo_by_id_successfully()
        {
            GenerateNewFakeDatabase("get_todo");

            int todoId = 1;

            Todo todo = new Todo()
            {
                Id = todoId,
                Title = "Todo 1",
                FolderId = 1
            };

            context.Todos.Add(todo);
            context.SaveChanges();

            Todo result = todoDal.GetTodo(todoId);
            result.Should().NotBeNull();
            
        }

        [Fact]
        public void get_todos_by_folder_id_successfully()
        {
            GenerateNewFakeDatabase("get_todos");

            int folderId = 1;
            int todoCountInTheFolder = 2;

            List<Todo> todos = new List<Todo>(){
                new Todo("Todo 1", folderId),
                new Todo("Todo 2", folderId),
                new Todo("Todo 1", 2)
            };

            context.Todos.AddRange(todos);
            context.SaveChanges();
           
            List<Todo> result = todoDal.GetTodos(folderId);
            result.Count.Should().Be(todoCountInTheFolder);      
        }

        [Fact]
        public void add_todo_successfully()
        {
            GenerateNewFakeDatabase("add_todo");
            
            Todo todo = new Todo("Todo 1", 1);
           
            Todo result = todoDal.AddTodo(todo);
            result.Should().NotBeNull();     
        }

        [Fact]
        public void remove_todo_by_id_successfully()
        {
            GenerateNewFakeDatabase("remove_todo");

            int todoId = 1;

            Todo todo = new Todo()
            {
                Id = todoId,
                Title = "Todo 1",
                FolderId = 1 
            };
            context.Todos.Add(todo);
            context.SaveChanges();

            bool result = todoDal.RemoveTodo(todoId);
            result.Should().BeTrue();      
        }

        [Fact]
        public void make_todo_done_successfully()
        {
            GenerateNewFakeDatabase("todo_done");

            int todoId = 1;

            Todo todo = new Todo()
            {
                Id = todoId,
                Title = "Todo 1",
                FolderId = 1,
                IsDone = false
            };
            context.Todos.Add(todo);
            context.SaveChanges();

            Todo result = todoDal.TodoDone(todoId);
            result.Should().NotBeNull();      
        }


        [Fact]
        public void remove_todos_by_folder_id_successfully()
        {
            GenerateNewFakeDatabase("remove_todos");

            int folderId = 1;

            List<Todo> todos = new List<Todo>(){
                new Todo("Todo 1", folderId),
                new Todo("Todo 2", folderId),
                new Todo("Todo 1", 2)
            };

            context.Todos.AddRange(todos);
            context.SaveChangesAsync();
           
            todoDal.RemoveTodosByFolderId(folderId);     
        }

    }
}