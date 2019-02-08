using TodoApp.API.Models;

namespace TodoApp.API.Managers.Abstraction
{
    public interface IUserManager
    {   
        User SignIn(User model);
        User Register(User model);
    }
}