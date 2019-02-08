using FluentAssertions;
using TodoApp.API.Helper.Authentication;
using Xunit;

namespace TodoApp.TEST.HelperClassTests.Authentication
{
    public class AuthServiceTests
    {
        [Fact]
        public void creating_token_should_work_properly()
        {
            int userId = 1;
            AuthService authService = new AuthService();
            string result = authService.CreateToken(userId);
            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void getting_user_id_should_work_properly_if_token_is_valid()
        {
            int userId = 1;
            AuthService authService = new AuthService();
            string token = authService.CreateToken(userId);

            int result = authService.GetUserId(token);
            result.Should().NotBe(0);
        }

        [Fact]
        public void getting_user_id_should_fail_if_token_is_not_valid()
        {
            int userId = 1;
            AuthService authService = new AuthService();
            authService.CreateToken(userId);

            int result = authService.GetUserId("invalid_token");
            result.Should().Be(0);
        }

    }
}