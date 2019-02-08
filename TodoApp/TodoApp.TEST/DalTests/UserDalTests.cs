using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using TodoApp.API.Abstraction.DataAccessLayers;
using TodoApp.API.Data;
using TodoApp.API.DataAccessLayers.Implementation;
using TodoApp.API.Helper;
using TodoApp.API.Managers.Abstraction;
using TodoApp.API.Managers.Implementation;
using TodoApp.API.Models;
using Xunit;

namespace TodoApp.TEST.DalTests
{
    public class UserDalTests
    {
        private DataContext context;
        private readonly Mock<IHasher> _hasher;
        private UserDal userDal;
        
        public UserDalTests()
        {
            _hasher = new Mock<IHasher>();
        }

        public void GenerateNewFakeDatabase(string databaseName)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;
            context = new DataContext(options);      
            userDal = new UserDal(context, _hasher.Object);
        }
        
        [Theory]
        [InlineData(1, "mert", true)]
        [InlineData(2, "notexistinguser", false)]
        public void check_if_user_exists(int id, string username, bool existence)
        {
            GenerateNewFakeDatabase("check_user_existence");

            User user = new User()
            {
                Id = id,
                Username = "mert"
            };

            context.Users.Add(user);
            context.SaveChanges();

            bool result = userDal.CheckIfUserExists(username);
            result.Should().Be(existence);
            
        }

        [Fact]
        public void create_user_successfully()
        {
            GenerateNewFakeDatabase("create_user");
            
            context.Users.Add(new User());
            context.SaveChanges();
            
            string username = "testuser";
            string password = "testpassword";

            User result = userDal.CreateUser(username, password);
            result.Should().NotBeNull();     
        }


        [Fact]
        public void get_user_by_username_and_password_successfully()
        {
            GenerateNewFakeDatabase("get_user");

            string username = "testuser";
            string password = "testpassword";

            User dbUser = new User()
            {
                Id = 1,
                Username = username,
                Password = "hashedPassword"
            };

            _hasher.Setup(x => x.getHashedPassword(password)).Returns(dbUser.Password);

            context.Users.Add(dbUser);
            context.SaveChanges();
            
            User result = userDal.GetUser(username, password);
            result.Should().NotBeNull();     
        }

    }
}