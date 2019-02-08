using System;
using System.Collections.Generic;
using TodoApp.API.Models;

namespace TodoApp.API.Abstraction.DataAccessLayers
{
    public interface IFolderDal
    {
        Folder GetFolder(int id);
        List<Folder> GetFolders(int userId);
        Folder AddFolder(Folder folder);
        bool RemoveFolder(int id);
    }
}