namespace TodoApp.API.Helper.Authentication
{
    public interface IAuthService
    {
        string CreateToken(int userId);
        int GetUserId(string token);
    } 
}