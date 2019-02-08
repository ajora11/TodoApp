using System.Collections.Generic;
using TodoApp.API.Models;

namespace TodoApp.API.Managers.Abstraction
{
    public interface IFolderManager
    {
        Folder AddFolder(string folderName, int userId);
        bool RemoveFolder(int id, int userId);
        List<Folder> GetFolders(int userId); 
        void CreateRecentFolder(int userId);
    }
}