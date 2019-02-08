using TodoApp.API.Models;

namespace TodoApp.API.Helper
{
    public interface IUserValidator
    {
        bool IsUserValid(User user);
    }
}