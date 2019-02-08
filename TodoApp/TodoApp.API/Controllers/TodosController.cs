using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.API.Data;
using TodoApp.API.Managers.Abstraction;
using TodoApp.API.Models;
using TodoApp.API.Managers.Implementation;
using TodoApp.API.Helper.Authentication;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoManager _todoManager;
        private readonly IAuthService _authService;

        public TodosController (ITodoManager todoManager, IAuthService authService)
        {
            _todoManager = todoManager;
            _authService = authService;
        }

        [Route("add")]
        [HttpGet]
        public IActionResult AddTodo(string todoName, int folderId)
        {
            string userToken = Request.Headers["token"];
            int userId = _authService.GetUserId(userToken);
            if(userId > 0)
            {
                Todo todo = _todoManager.AddTodo(todoName, folderId, userId);
                return Ok(todo);
            }

            return Unauthorized();
        }

        [Route("remove")]
        [HttpGet]
        public IActionResult RemoveTodo(int todoId)
        {
            string userToken = Request.Headers["token"];
            int userId = _authService.GetUserId(userToken);
            if(userId > 0)
            {
                bool result = _todoManager.RemoveTodo(todoId, userId);
                return Ok(result);
            }

            return Unauthorized();
        }
       
        [HttpGet]
        public IActionResult GetTodos(int folderId)
        {
            List<Todo> todos = _todoManager.GetTodos(folderId);
            return Ok(todos);
        }

        [Route("done")]
        [HttpGet]
        public IActionResult TodoDone(int id)
        {
            string userToken = Request.Headers["token"];
            int userId = _authService.GetUserId(userToken);
            if(userId > 0)
            {
                Todo todo = _todoManager.TodoDone(id, userId);
                return Ok(todo);
            } 
            
            return Unauthorized();
        }
    }
}
