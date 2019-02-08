using System.Collections.Generic;
using TodoApp.API.Models;

namespace TodoApp.API.Abstraction.DataAccessLayers
{
    public interface IUserDal
    {
        User GetUser(string username, string password);
        User CreateUser(string username, string password);
        bool CheckIfUserExists(string username);
    }
}