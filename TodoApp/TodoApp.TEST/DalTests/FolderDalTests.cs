using System.Collections.Generic;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TodoApp.API.Data;
using TodoApp.API.DataAccessLayers.Implementation;
using TodoApp.API.Models;
using Xunit;

namespace TodoApp.TEST.DalTests
{
    public class FolderDalTests
    {
        private DataContext context;
        private FolderDal folderDal;

        public void GenerateNewFakeDatabase(string databaseName){
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;

            context = new DataContext(options);
            folderDal = new FolderDal(context);
        }

        
        [Fact]
        public void add_folder_successfully()
        {
            GenerateNewFakeDatabase("add_folder");
            
            Folder folder = new Folder("Todo 1", 1);
           
            Folder result = folderDal.AddFolder(folder);
            result.Should().NotBeNull();     
        }

        [Fact]
        public void get_folder_by_idsuccessfully()
        {
            GenerateNewFakeDatabase("get_folder");
            
            int folderId = 1;

            Folder folder = new Folder()
            {
                Id = folderId,
                Title = "New folder"
            };

            context.Folders.Add(folder);
            context.SaveChanges();

            Folder result = folderDal.GetFolder(folderId);
            result.Should().NotBeNull();     
        }


        [Fact]
        public void get_folders_by_user_id_successfully()
        {
            GenerateNewFakeDatabase("get_folders");

            int userId = 1;
            int folderCountOfUser = 2;

            List<Folder> folders = new List<Folder>(){
                new Folder("List 1", userId),
                new Folder("List 2", userId),
                new Folder("List 1", 2)
            };

            context.Folders.AddRange(folders);
            context.SaveChanges();
           
            List<Folder> result = folderDal.GetFolders(userId);
            result.Count.Should().Be(folderCountOfUser);      
        }

        [Fact]
        public void remove_folder_by_id_successfully()
        {
            GenerateNewFakeDatabase("remove_folder");

            int folderId = 1;

            Folder folder = new Folder()
            {   
                Id = folderId,
                Title = "List"
            };

            context.Folders.Add(folder);
            context.SaveChanges();
           
            bool result = folderDal.RemoveFolder(folderId);
            result.Should().BeTrue();      
        }
    }
}