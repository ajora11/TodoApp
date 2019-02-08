using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApp.API.Helper.Authentication;
using TodoApp.API.Managers.Abstraction;
using TodoApp.API.Models;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoldersController : ControllerBase
    {
        private readonly IFolderManager _folderManager;
        private readonly IAuthService _authService;
        public FoldersController (IFolderManager folderManager, IAuthService authService)
        {
            _folderManager = folderManager;
            _authService = authService;
        }

        [HttpGet]
        public IActionResult GetFolders()
        {
            string userToken = Request.Headers["token"];
            int userId = _authService.GetUserId(userToken);
            if(userId > 0)
            {
                List<Folder> folders = _folderManager.GetFolders(userId);
                return Ok(folders);
            }

            return Unauthorized();
            
        }

        [Route("add")]
        [HttpGet]
        public IActionResult AddNewFolder(string folderName)
        {
            string userToken = Request.Headers["token"];
            int userId = _authService.GetUserId(userToken);
            if(userId > 0)
            {
                Folder folder = _folderManager.AddFolder(folderName, userId);
                return Ok(folder);
            }

            return Unauthorized();
        }

        [Route("remove")]
        [HttpGet]
        public IActionResult RemoveFolder(int folderId)
        {
            string userToken = Request.Headers["token"];
            int userId = _authService.GetUserId(userToken);
            if(userId > 0)
            {
                // userId is being passed to evade attacks, for more check RemoveFolder func
                bool result = _folderManager.RemoveFolder(folderId, userId);
                return Ok(result);
            }
            return Unauthorized();
        }
    }
}
