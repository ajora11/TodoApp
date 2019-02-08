using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApp.API.Controllers;
using TodoApp.API.Helper.Authentication;
using TodoApp.API.Managers.Abstraction;
using TodoApp.API.Models;
using Xunit;

namespace TodoApp.TEST.ControllerTests
{
    public class TodosControllerTests
    {
        private readonly Mock<ITodoManager> _todoManager;
        private readonly Mock<IAuthService> _authService;
        private readonly TodosController _todosController;
        public TodosControllerTests()
        {
            _todoManager = new Mock<ITodoManager>();
            _authService = new Mock<IAuthService>();
            _todosController = new TodosController(_todoManager.Object, _authService.Object);

            _todosController.ControllerContext = new ControllerContext();
            _todosController.ControllerContext.HttpContext = new DefaultHttpContext();
            _todosController.ControllerContext.HttpContext.Request.Headers["token"] = "";  
        }

        [Fact]
        public void adding_todo_returns_unauthorized_if_user_id_is_not_valid(){
            string todoName = "My todo";
            int folderId = 1;
            int invalidUserId = 0;

            _authService.Setup(x => x.GetUserId(It.IsAny<string>())).Returns(invalidUserId);

            IActionResult result = _todosController.AddTodo(todoName, folderId);
            result.Should().BeOfType(typeof(UnauthorizedResult));
        }

        [Fact]
        public void adding_todo_returns_ok_if_user_id_is_valid(){
            string todoName = "My todo";
            int folderId = 1;
            int validUserId = 5;

            _authService.Setup(x => x.GetUserId(It.IsAny<string>())).Returns(validUserId);

            Todo todo = new Todo();
            _todoManager.Setup(x => x.AddTodo(todoName, folderId, validUserId)).Returns(todo);

            IActionResult result = _todosController.AddTodo(todoName, folderId);
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void removing_todo_returns_unauthorized_if_user_id_is_not_valid(){
            int todoId = 1;
            int invalidUserId = 0;
            
            _authService.Setup(x => x.GetUserId(It.IsAny<string>())).Returns(invalidUserId);

            IActionResult result = _todosController.RemoveTodo(todoId);
            result.Should().BeOfType(typeof(UnauthorizedResult));
        }
        
        [Fact]
        public void removing_todo_returns_ok_if_user_id_is_valid(){
            int todoId = 1;
            int validUserId = 5;

            _authService.Setup(x => x.GetUserId(It.IsAny<string>())).Returns(validUserId);

            _todoManager.Setup(x => x.RemoveTodo(todoId, validUserId)).Returns(true);

            IActionResult result = _todosController.RemoveTodo(todoId);
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void get_todos_by_folder_id_successfully(){
            int folderId = 1;

            List<Todo> todos = new List<Todo>();
            _todoManager.Setup(x => x.GetTodos(folderId)).Returns(todos);

            IActionResult result = _todosController.GetTodos(folderId);
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void making_todo_done_returns_unauthorized_if_user_id_is_not_valid(){
            int todoId = 1;
            int invalidUserId = 0;

            _authService.Setup(x => x.GetUserId(It.IsAny<string>())).Returns(invalidUserId);

            IActionResult result = _todosController.TodoDone(todoId);
            result.Should().BeOfType(typeof(UnauthorizedResult));
        }

         [Fact]
        public void making_todo_done_returns_ok_if_user_id_is_valid(){
            int todoId = 1;
            int validUserId = 5;

            _authService.Setup(x => x.GetUserId(It.IsAny<string>())).Returns(validUserId);

            Todo todo = new Todo();
            _todoManager.Setup(x => x.TodoDone(todoId, validUserId)).Returns(todo);

            IActionResult result = _todosController.TodoDone(todoId);
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}