using FluentAssertions;
using Moq;
using TodoApp.API.Abstraction.DataAccessLayers;
using TodoApp.API.Helper;
using TodoApp.API.Managers.Implementation;
using TodoApp.API.Models;
using Xunit;

namespace TodoApp.TEST.HelperClassTests
{
    public class UserValidatorTests
    {
        [Fact]
        public void empty_username_should_not_be_accepted()
        {
            UserValidator userValidator = new UserValidator();
            bool result = userValidator.IsUsernameValid("");
            result.Should().BeFalse();
        }
        
        [Fact]
        public void empty_password_should_not_be_accepted()
        {
            UserValidator userValidator = new UserValidator();
            bool result = userValidator.IsPasswordValid("");
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("testname", "")]
        [InlineData("", "testpassword")]
        public void user_with_invalid_fields_should_not_be_accepted(string username, string password)
        {
            UserValidator userValidator = new UserValidator();
            
            User user = new User(){
                Username = username,
                Password = password
            };
 
            bool result = userValidator.IsUserValid(user);
            result.Should().Be(false);
        }
        
    }
}