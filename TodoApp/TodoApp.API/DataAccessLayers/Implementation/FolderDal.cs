using System;
using System.Collections.Generic;
using System.Linq;
using TodoApp.API.Abstraction.DataAccessLayers;
using TodoApp.API.Data;
using TodoApp.API.Models;

namespace TodoApp.API.DataAccessLayers.Implementation
{
    public class FolderDal : IFolderDal
    {   
        private readonly DataContext _context;
        public FolderDal (DataContext context)
        {
            _context = context;
        }

        public Folder AddFolder(Folder folder)
        {
            folder = _context.Folders.Add(folder).Entity;
            _context.SaveChanges();
            return folder;
        }

        public Folder GetFolder(int id)
        {
            return _context.Folders.FirstOrDefault(f => f.Id == id);
        }

        public List<Folder> GetFolders(int userId)
        {
            return _context.Folders.Where(f => f.UserId == userId).ToList();
        }

        public bool RemoveFolder(int id)
        {
            _context.Folders.Remove(GetFolder(id));
            _context.SaveChanges();
            return true;
        }
    }
}