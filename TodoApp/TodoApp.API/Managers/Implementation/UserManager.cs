using TodoApp.API.Abstraction.DataAccessLayers;
using TodoApp.API.Helper;
using TodoApp.API.Managers.Abstraction;
using TodoApp.API.Models;

namespace TodoApp.API.Managers.Implementation
{
    public class UserManager : IUserManager
    {
        private readonly IUserDal _userDal;
        private readonly IUserValidator _userValidator;
        private readonly IFolderManager _folderManager;
        public UserManager(IUserDal userDal, IUserValidator userValidator, IFolderManager folderManager)
        {
            _userDal = userDal;
            _userValidator = userValidator;
            _folderManager = folderManager;
        }

        public User SignIn(User model){
            
            if(!_userValidator.IsUserValid(model))
                return null;
            
            return _userDal.GetUser(model.Username, model.Password);     
        }

        public User Register(User model)
        {
            if(!_userValidator.IsUserValid(model))
                return null;

            if(_userDal.CheckIfUserExists(model.Username))
                return null;

            User user = _userDal.CreateUser(model.Username, model.Password);
                        
            _folderManager.CreateRecentFolder(user.Id);

            return user;
        }
    }
}