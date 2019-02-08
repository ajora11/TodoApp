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
    public class UserManagerTests
    {
        private readonly Mock<IUserDal> _userDal;
        private readonly Mock<IUserValidator> _userValidator;
        private readonly Mock<IFolderManager> _folderManager;
        private readonly UserManager _userManager;

        public UserManagerTests()
        {
            _userDal = new Mock<IUserDal>();
            _userValidator = new Mock<IUserValidator>();
            _folderManager = new Mock<IFolderManager>();
            _userManager = new UserManager(_userDal.Object, _userValidator.Object, _folderManager.Object);
        }
        [Fact]
        public void valid_user_should_be_registered_successfully()
        {
            User model = new User(){
                Username = "testname",
                Password = "testpassword"
            };

            User dbUser = new User()
            {
                Id = 100,
                Username = "testname",
                Password = "testpassword"
            }; 
            
            _userValidator.Setup( x => x.IsUserValid(model)).Returns(true);
            _userDal.Setup(x => x.CheckIfUserExists(model.Username)).Returns(false);
            _userDal.Setup(x => x.CreateUser(model.Username, model.Password)).Returns(dbUser);
            _folderManager.Setup(x => x.CreateRecentFolder(dbUser.Id));

            User result = _userManager.Register(model);

            result.Should().NotBeNull();
        }


        [Theory]
        [InlineData("", "")]
        [InlineData("testname", "")]
        [InlineData("", "testpassword")]
        public void invalid_user_should_not_be_registered(string username, string password)
        {
            User model = new User(){
                Username = username,
                Password = password
            };
            
            _userValidator.Setup( x => x.IsUserValid(model)).Returns(false);

            User result = _userManager.Register(model);

            result.Should().BeNull();
        }

        [Fact]
        public void user_with_an_existing_name_should_not_be_registered()
        { 
            _userValidator.Setup( x => x.IsUserValid(It.IsNotNull<User>())).Returns(true);
            _userDal.Setup(x => x.CheckIfUserExists(It.IsNotNull<string>())).Returns(true);

            User user = new User(){
                Username = "testname",
                Password = "1234"
            };
            
            User result = _userManager.Register(user);

            result.Should().BeNull();
        }

        [Fact]
        public void valid_user_should_be_signed_in_successfully()
        {
            User model = new User(){
                Username = "testname",
                Password = "testpassword"
            };

            User dbUser = new User()
            {
                Id = 100,
                Username = "testname",
                Password = "testpassword"
            }; 
            
            _userValidator.Setup( x => x.IsUserValid(model)).Returns(true);
            _userDal.Setup(x => x.GetUser(model.Username, model.Password)).Returns(dbUser);

            User result = _userManager.SignIn(model);

            result.Should().NotBeNull();
        }
 
        [Theory]
        [InlineData("", "")]
        [InlineData("testname", "")]
        [InlineData("", "testpassword")]
        public void invalid_user_should_not_be_signed_in(string username, string password)
        {
            User model = new User(){
                Username = username,
                Password = password
            };

            User dbUser = new User()
            {
                Id = 100,
                Username = username,
                Password = password
            }; 
            
            _userValidator.Setup( x => x.IsUserValid(model)).Returns(false);
            _userDal.Setup(x => x.GetUser(model.Username, model.Password)).Returns(dbUser);

            User result = _userManager.SignIn(model);

            result.Should().BeNull();
        }
    }
}