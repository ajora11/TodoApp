using System.Collections.Generic;
using FluentAssertions;
using Moq;
using TodoApp.API.Abstraction.DataAccessLayers;
using TodoApp.API.Helper;
using TodoApp.API.Managers.Abstraction;
using TodoApp.API.Managers.Implementation;
using TodoApp.API.Models;
using Xunit;

namespace TodoApp.TEST.ManagerTests
{
    public class TodoManagerTests
    {
        private readonly Mock<ITodoDal> _todoDal;
        private readonly Mock<IFolderDal> _folderDal;
        private readonly TodoManager _todoManager;

        public TodoManagerTests()
        {
            _todoDal = new Mock<ITodoDal>();
            _folderDal = new Mock<IFolderDal>();
            _todoManager = new TodoManager(_todoDal.Object, _folderDal.Object);
        }

        [Fact]
        public void get_todos_by_folder_id_successfully()
        {
            int folderId = 1;
            
            List<Todo> todos=new List<Todo>();
            _todoDal.Setup(x => x.GetTodos(folderId)).Returns(todos);

            List<Todo> result = _todoManager.GetTodos(folderId);
            result.Should().NotBeNull();
        }

        [Fact]
        public void adding_todo_should_fail_if_userId_of_folder_does_not_match_userId()
        {
            string todoName = "todoname";
            int sessionUserId = 7;
            
            Folder dbFolder=new Folder(){
                Id = 1,
                Title = "foldertitle",
                UserId = 8
            };

            _folderDal.Setup(x => x.GetFolder(dbFolder.Id)).Returns(dbFolder);

            Todo result = _todoManager.AddTodo(todoName, dbFolder.Id, sessionUserId);
            result.Should().BeNull();
        }

        [Fact]
        public void adding_todo_should_success_if_userId_of_folder_matches_userId()
        {
            string todoName = "todoname";
            int sessionUserId = 7;
            
            Folder dbFolder=new Folder(){
                Id = 1,
                Title = "foldertitle",
                UserId = sessionUserId
            };

            
            _folderDal.Setup(x => x.GetFolder(dbFolder.Id)).Returns(dbFolder);

            Todo todo = new Todo();
            _todoDal.Setup(x => x.AddTodo(It.IsAny<Todo>())).Returns(todo);
            
            Todo result = _todoManager.AddTodo(todoName, dbFolder.Id, sessionUserId);
            result.Should().NotBeNull();
        }

        [Fact]
        public void removing_todo_should_fail_if_userId_of_folder_does_not_match_userId()
        {
            int todoId = 1;
            int userId = 1;
           
            Todo dbTodo=new Todo()
            {
                Id = todoId,
                FolderId = 5,
            };

            Folder dbFolder = new Folder(){
                Id = dbTodo.FolderId,
                Title = "foldertitle",
                UserId = 8
            };

            _todoDal.Setup(x => x.GetTodo(It.IsNotNull<int>())).Returns(dbTodo);

            _folderDal.Setup(x => x.GetFolder(dbTodo.FolderId)).Returns(dbFolder);

            bool result = _todoManager.RemoveTodo(todoId, userId);
            result.Should().BeFalse();
        }

        [Fact]
        public void removing_todo_should_success_if_userId_of_folder_matches_userId()
        {
            int todoId = 1;
            int userId = 1;
           
            Todo dbTodo=new Todo()
            {
                Id = todoId,
                FolderId = 5,
            };

            Folder dbFolder = new Folder(){
                Id = dbTodo.FolderId,
                Title = "foldertitle",
                UserId = userId
            };

            _todoDal.Setup(x => x.GetTodo(It.IsNotNull<int>())).Returns(dbTodo);

            _folderDal.Setup(x => x.GetFolder(dbTodo.FolderId)).Returns(dbFolder);

            _todoDal.Setup(x => x.RemoveTodo(It.IsNotNull<int>())).Returns(true);

            bool result = _todoManager.RemoveTodo(todoId, userId);
            result.Should().BeTrue();
        }

        [Fact]
        public void setting_todo_as_done_should_fail_if_todoId_is_invalid()
        {
            int todoId = 1;
            int userId = 1;
           
            Todo todo = null;
            _todoDal.Setup(x => x.GetTodo(It.IsNotNull<int>())).Returns(todo);

            
            Todo result = _todoManager.TodoDone(todoId, userId);
            result.Should().BeNull();
        }

        [Fact]
        public void setting_todo_as_done_should_fail_if_todoId_is_valid_but_if_userId_of_todo_does_not_match_userId()
        {
            int todoId = 1;
            int userId = 1;
           
            Todo dbTodo = new Todo()
            {
                Id = todoId,
                FolderId = 5,
                IsDone = false
            };
            Folder dbFolder = new Folder()
            {
                Id = dbTodo.FolderId,
                Title = "foldertitle",
                UserId = 2
            };

            _todoDal.Setup(x => x.GetTodo(It.IsNotNull<int>())).Returns(dbTodo);

            _folderDal.Setup(x => x.GetFolder(dbTodo.FolderId)).Returns(dbFolder);
            
            Todo result = _todoManager.TodoDone(todoId, userId);
            result.Should().BeNull();
        }

        [Fact]
        public void setting_todo_as_done_should_success()
        {
            int todoId = 1;
            int userId = 1;
           
            Todo dbTodo = new Todo()
            {
                Id = todoId,
                FolderId = 5,
                IsDone = false
            };
            Folder dbFolder = new Folder()
            {
                Id = dbTodo.FolderId,
                Title = "foldertitle",
                UserId = userId
            };

            _todoDal.Setup(x => x.GetTodo(It.IsNotNull<int>())).Returns(dbTodo);

            _folderDal.Setup(x => x.GetFolder(dbTodo.FolderId)).Returns(dbFolder);
            
            _todoDal.Setup(x => x.TodoDone(dbTodo.Id)).Returns(dbTodo);

            Todo result = _todoManager.TodoDone(todoId, userId);
            result.Should().NotBeNull();
        }

        [Fact]
        public void initial_todos_should_be_created_successfully()
        {
            int folderId = 1;

            _todoDal.Setup(x => x.AddTodo(It.IsNotNull<Todo>()));

            _todoManager.CreateInitialTodos(folderId);
        }
    }
}