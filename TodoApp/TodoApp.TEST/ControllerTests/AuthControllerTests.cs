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
    public class AuthControllerTests
    {
        private readonly Mock<IUserManager> _userManager;
        private readonly Mock<IAuthService> _authService;
        private readonly AuthController _authController;
        public AuthControllerTests()
        {
            _userManager = new Mock<IUserManager>();
            _authService = new Mock<IAuthService>();
            _authController = new AuthController(_userManager.Object, _authService.Object);

            _authController.ControllerContext = new ControllerContext();
            _authController.ControllerContext.HttpContext = new DefaultHttpContext();
            _authController.ControllerContext.HttpContext.Request.Headers["token"] = "test";  
        }

        [Fact]
        public void user_signs_in_if_sign_in_model_is_valid(){
            User model = new User()
            {
                Username = "mert",
                Password = "1234"
            };

            User dbUser = new User()
            {
                Id = 1,
                Username = "mert",
                Password = "1234"
            };

            _userManager.Setup(x => x.SignIn(model)).Returns(dbUser);
            
            _authService.Setup(x => x.CreateToken(dbUser.Id)).Returns("token");

            IActionResult result = _authController.SignIn(model);
            ((OkObjectResult)result).Value.Should().NotBeNull();   
        }

        [Fact]
        public void user_signing_in_is_rejected_if_sign_in_model_is_not_valid(){
            User model = new User()
            {
                Username = "",
                Password = ""
            };

            User user = null;
            _userManager.Setup(x => x.SignIn(model)).Returns(user);

            IActionResult result = _authController.SignIn(model);
            ((OkObjectResult)result).Value.Should().BeNull(); 
        }

        [Fact]
        public void user_registers_if_register_model_is_valid(){
            
            User model = new User()
            {
                Username = "mert",
                Password = "1234"
            };

            User dbUser = new User()
            {
                Id = 1,
                Username = "mert",
                Password = "1234"
            };

            _userManager.Setup(x => x.Register(model)).Returns(dbUser);
            
            _authService.Setup(x => x.CreateToken(dbUser.Id)).Returns("token");

            IActionResult result = _authController.Register(model);
            ((OkObjectResult)result).Value.Should().NotBeNull();    
        }
        
        [Fact]
        public void user_register_is_rejected_if_register_model_is_not_valid(){
            User model = new User()
            {
                Username = "",
                Password = ""
            };

            User user = null;
            _userManager.Setup(x => x.Register(model)).Returns(user);

            IActionResult result = _authController.Register(model);
            ((OkObjectResult)result).Value.Should().BeNull();  
        } 
    }
}