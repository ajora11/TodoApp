using System.Collections.Generic;
using FluentAssertions;
using Moq;
using TodoApp.API.Abstraction.DataAccessLayers;
using TodoApp.API.Helper;
using TodoApp.API.Managers.Abstraction;
using TodoApp.API.Managers.Implementation;
using TodoApp.API.Models;
using Xunit;

namespace TodoApp.TEST.ManagerTests
{
    public class FolderManagerTests
    {
        private readonly Mock<IFolderDal> _folderDal;
        private readonly Mock<ITodoDal> _todoDal;
        private readonly Mock<ITodoManager> _todoManager;
        private readonly FolderManager _folderManager;

        public FolderManagerTests()
        {
            _folderDal = new Mock<IFolderDal>();
            _todoDal = new Mock<ITodoDal>();
            _todoManager = new Mock<ITodoManager>();
            _folderManager = new FolderManager(_folderDal.Object, _todoDal.Object, _todoManager.Object);
        }

        [Fact]
        public void folder_should_be_added_successfully()
        {
            string folderTitle = "foldertitle";
            int userId = 1;

            Folder dbFolder = new Folder()
            {
                Id = 1,
                Title = folderTitle,
                UserId = userId
            };

            _folderDal.Setup(x => x.AddFolder(It.IsNotNull<Folder>())).Returns(dbFolder);
            _todoDal.Setup(x => x.AddTodo(It.IsNotNull<Todo>()));

            Folder result = _folderManager.AddFolder(folderTitle, userId);
            result.Should().NotBeNull();
        }

        [Fact]
        public void user_folders_should_be_gotten_by_user_id()
        {
            int userId = 1;

            List<Folder> folderList = new List<Folder>();
            _folderDal.Setup(x => x.GetFolders(userId)).Returns(folderList);
            _todoDal.Setup(x => x.AddTodo(It.IsNotNull<Todo>()));

            List<Folder> result = _folderManager.GetFolders(userId);
            result.Should().NotBeNull();
        }

        [Fact]
        public void removing_folder_should_fail_if_folder_id_is_invalid()
        {
            int folderId = 1;
            int userId = 1;
            
            bool result = _folderManager.RemoveFolder(folderId, userId);
            result.Should().BeFalse();
        }

        [Fact]
        public void removing_folder_should_fail_if_folder_id_is_valid_but_user_id_of_folder_does_not_match_user_id()
        {
            int folderId = 1;
            int userId = 1;

            Folder dbFolder = new Folder()
            {
                Id = 1,
                Title = "foldertitle",
                UserId = 2
            };

            _folderDal.Setup(x => x.GetFolder(folderId)).Returns(dbFolder);

            bool result = _folderManager.RemoveFolder(folderId, userId);
            result.Should().BeFalse();
        }

        [Fact]
        public void folder_should_be_removed_successfully()
        {
            int folderId = 1;
            int userId = 1;

            Folder dbFolder = new Folder()
            {
                Id = 1,
                Title = "foldertitle",
                UserId = userId
            };

            _folderDal.Setup(x => x.GetFolder(folderId)).Returns(dbFolder);

            _todoDal.Setup(x => x.RemoveTodosByFolderId(folderId));
            _folderDal.Setup(x => x.RemoveFolder(folderId));

            bool result = _folderManager.RemoveFolder(folderId, userId);
            result.Should().BeTrue();
        }

        [Fact]
        public void recent_folder_should_be_created_successfully()
        {
            int userId = 1;

            Folder dbFolder = new Folder()
            {
                Id = 1,
                Title = "foldertitle",
                UserId = userId
            };

            _folderDal.Setup(x => x.AddFolder(It.IsNotNull<Folder>())).Returns(dbFolder);

            _todoManager.Setup(x => x.CreateInitialTodos(dbFolder.Id));

            _folderManager.CreateRecentFolder(userId);
        }
    }
}