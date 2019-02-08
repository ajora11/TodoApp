using System;
using System.Collections.Generic;

namespace TodoApp.API.Helper.Authentication
{
    public class AuthService: IAuthService
    {
        private readonly Dictionary<string, int> tokens = new Dictionary<string, int>();
        
        public string CreateToken(int userId){
            string token = Guid.NewGuid().ToString();
            tokens.Add(token, userId);
            return token;
        }

        public int GetUserId(string token){
            if(tokens.ContainsKey(token)) 
            {
                return tokens[token];
            }
            return 0;
        }
    }
}