using System.Collections.Generic;
using TodoApp.API.Abstraction.DataAccessLayers;
using TodoApp.API.Managers.Abstraction;
using TodoApp.API.Models;

namespace TodoApp.API.Managers.Implementation
{
    public class FolderManager : IFolderManager
    {
        private readonly IFolderDal _folderDal;
        private readonly ITodoDal _todoDal;
        private readonly ITodoManager _todoManager;

        public FolderManager(IFolderDal folderDal, ITodoDal todoDal, ITodoManager todoManager)
        {
            _folderDal = folderDal;
            _todoDal = todoDal;
            _todoManager = todoManager;
        }

        public Folder AddFolder(string folderName, int userId)
        {
            Folder folder = new Folder(folderName, userId);
            folder = _folderDal.AddFolder(folder);
            _todoDal.AddTodo(new Todo("Your first to-do", folder.Id));
            return folder;
        }

        public List<Folder> GetFolders(int userId)
        {
            return _folderDal.GetFolders(userId);
        }

        public bool RemoveFolder(int id, int userId)
        {
            Folder folder = _folderDal.GetFolder(id);
            
            if(folder == null)
                return false;
            
            if(folder.UserId != userId)
                return false;
        
            _todoDal.RemoveTodosByFolderId(folder.Id);
            _folderDal.RemoveFolder(folder.Id);
            return true;
            
        }

        public void CreateRecentFolder(int userId)
        {
            Folder folder = _folderDal.AddFolder(new Folder("Recent", userId));
            _todoManager.CreateInitialTodos(folder.Id);
        }
    }
}