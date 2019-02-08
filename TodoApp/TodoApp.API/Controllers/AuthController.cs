using TodoApp.API.Data;
using TodoApp.API.Managers;
using Microsoft.AspNetCore.Mvc;
using TodoApp.API.Models;
using TodoApp.API.Managers.Implementation;
using TodoApp.API.Managers.Abstraction;
using TodoApp.API.Helper.Authentication;
using System;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IAuthService _authService;
        public AuthController (IUserManager userManager, IAuthService authService)
        {
             _userManager = userManager;
             _authService = authService;
        }
 
        [HttpPost]
        public IActionResult SignIn([FromBody] User model)
        {
            User user = _userManager.SignIn(model); 
            if(user != null){
                string token = _authService.CreateToken(user.Id);
                Token tokenModel = new Token(token);
                return Ok(tokenModel);
            }

            return Ok(null);
        }

        [Route("register")]
        [HttpPost]
        public IActionResult Register([FromBody] User model)
        {
            User user = _userManager.Register(model); 
            if(user != null)
            {
                string token = _authService.CreateToken(user.Id);
                Token tokenModel = new Token(token);
                return Ok(tokenModel);
            }
            
            return Ok(null);
        }
    }
}
