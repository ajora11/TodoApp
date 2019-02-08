using FluentAssertions;
using TodoApp.API.Helper;
using Xunit;

namespace TodoApp.TEST.HelperClassTests
{
    public class HasherTests
    {
        [Fact]
        public void password_hashing_should_work_properly()
        {
            Hasher hasher = new Hasher();
            string result = hasher.getHashedPassword("password");
            result.Should().NotBeNullOrEmpty();
        }
    }
}