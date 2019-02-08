using System.Security.Cryptography;
using System.Text;

namespace TodoApp.API.Helper
{
    public class Hasher : IHasher
    {
        public string getHashedPassword(string password)
        {
            byte[] data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++) 
                sBuilder.Append(data[i].ToString("x2"));
                
            return sBuilder.ToString();
        }
    }
}