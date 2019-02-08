using TodoApp.API.Models;

namespace TodoApp.API.Helper
{
    public class UserValidator : IUserValidator
    {
        public bool IsUserValid(User user){
            return IsUsernameValid(user.Username) && IsPasswordValid(user.Password);
        }

        public bool IsUsernameValid(string username) {
            return username.Length > 1;
        }

        public bool IsPasswordValid(string password) {
            return password.Length > 1;
        }
    }
}