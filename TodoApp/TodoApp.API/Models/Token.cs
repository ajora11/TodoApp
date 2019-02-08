namespace TodoApp.API.Models
{
    public class Token
    {
        public string token { get; set; }
        public Token(string token)
        {
            this.token = token;
        }
    }
}