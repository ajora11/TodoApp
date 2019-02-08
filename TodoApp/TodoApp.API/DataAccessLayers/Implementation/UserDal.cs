using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TodoApp.API.Abstraction.DataAccessLayers;
using TodoApp.API.Data;
using TodoApp.API.Helper;
using TodoApp.API.Models;

namespace TodoApp.API.DataAccessLayers.Implementation
{
    public class UserDal : IUserDal
    {
        private readonly DataContext _context;
        private readonly IHasher _hasher;

        public UserDal (DataContext context, IHasher hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        public bool CheckIfUserExists(string username)
        {
            if(_context.Users.Where(u => u.Username.ToLower().Equals(username.ToLower())).ToList().Count > 0)
                return true;
            else
                return false;
        }

        public User CreateUser(string username, string password)
        {
            User user = new User();
            user.Username = username;
            user.Password = _hasher.getHashedPassword(password);

            //giving error without setting id, this is a temp solution to this
            user.Id = _context.Users.OrderByDescending(u => u.Id).First().Id + 1;

            user = _context.Users.Add(user).Entity;
            
            _context.SaveChanges();
            return user;
        }

        public User GetUser(string username, string password)
        {
            password = _hasher.getHashedPassword(password);
            return _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

 
    }
}