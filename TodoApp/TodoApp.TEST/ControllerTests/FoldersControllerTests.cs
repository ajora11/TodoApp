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
    public class FoldersControllerTests
    {
        private readonly Mock<IFolderManager> _folderManager;
        private readonly Mock<IAuthService> _authService;
        private readonly FoldersController _foldersController;
        public FoldersControllerTests()
        {
            _folderManager = new Mock<IFolderManager>();
            _authService = new Mock<IAuthService>();
            _foldersController = new FoldersController(_folderManager.Object, _authService.Object);

            _foldersController.ControllerContext = new ControllerContext();
            _foldersController.ControllerContext.HttpContext = new DefaultHttpContext();
            _foldersController.ControllerContext.HttpContext.Request.Headers["token"] = "test";  
        }

        [Fact]
        public void get_user_folders_returns_unauthorized_if_token_is_not_valid(){
            string invalidToken = "";
            int invalidUserId = 0;

            _authService.Setup(x => x.GetUserId(invalidToken)).Returns(invalidUserId);

            IActionResult result = _foldersController.GetFolders();
            result.Should().BeOfType(typeof(UnauthorizedResult));   
        }

        [Fact]
        public void add_new_folder_returns_unauthorized_if_token_is_not_valid(){
            string folderName = "folder";
            string invalidToken = "";
            int invalidUserId = 0;

            _authService.Setup(x => x.GetUserId(invalidToken)).Returns(invalidUserId);

            IActionResult result = _foldersController.AddNewFolder(folderName);
            result.Should().BeOfType(typeof(UnauthorizedResult));   
        }

        [Fact]
        public void remove_folder_returns_unauthorized_if_token_is_not_valid(){
            int folderId = 1;
            string invalidToken = "";
            int invalidUserId = 0;

            _authService.Setup(x => x.GetUserId(invalidToken)).Returns(invalidUserId);

            IActionResult result = _foldersController.RemoveFolder(folderId);
            result.Should().BeOfType(typeof(UnauthorizedResult));   
        }

        [Fact]
        public void get_user_folders_returns_ok_if_token_is_valid(){
            int validUserId = 1;

            _authService.Setup(x => x.GetUserId(It.IsNotNull<string>())).Returns(validUserId);

            List<Folder> folders = new List<Folder>()
            {
                new Folder("foldertitle", 1)
            };

            _folderManager.Setup(x => x.GetFolders(validUserId)).Returns(folders);

            IActionResult result = _foldersController.GetFolders();
            result.Should().BeOfType(typeof(OkObjectResult));        
        }

        [Fact]
        public void add_new_folder_returns_ok_if_token_is_valid(){
            string folderName = "folder";
            int validUserId = 1;

            _authService.Setup(x => x.GetUserId(It.IsNotNull<string>())).Returns(validUserId);

            Folder folder = new Folder();

            _folderManager.Setup(x => x.AddFolder(folderName, validUserId)).Returns(folder);

            IActionResult result = _foldersController.AddNewFolder(folderName);
            result.Should().BeOfType(typeof(OkObjectResult));        
        }

        [Fact]
        public void remove_folder_returns_ok_if_token_is_valid(){
            int folderId = 1;
            int validUserId = 1;

            _authService.Setup(x => x.GetUserId(It.IsNotNull<string>())).Returns(validUserId);

            _folderManager.Setup(x => x.RemoveFolder(folderId, validUserId)).Returns(true);

            IActionResult result = _foldersController.RemoveFolder(folderId);
            result.Should().BeOfType(typeof(OkObjectResult));        
        }
    }
}