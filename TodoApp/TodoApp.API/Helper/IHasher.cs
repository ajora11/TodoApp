namespace TodoApp.API.Helper
{
    public interface IHasher
    {
         string getHashedPassword(string password);
    }
}