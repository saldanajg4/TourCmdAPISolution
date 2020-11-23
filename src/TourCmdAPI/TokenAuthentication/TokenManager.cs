using System;
using System.Collections.Generic;
using System.Linq;

namespace TourCmdAPI.TokenAuthentication
{

    public class TokenManager : ITokenManager
    {
        private List<Token> tokenList;
        public TokenManager()
        {
            tokenList = new List<Token>();
            tokenList.Add(new Token{
        Value ="9fc3b99a-3df8-4522-8b52-313087afc69f",
        ExpiryDate = Convert.ToDateTime("2020-11-10T12:45:26.0129295-06:00")
    });
        }
        public bool Authenticate(string userName, string password)
        {
            if (!string.IsNullOrWhiteSpace(userName) &&
                !string.IsNullOrWhiteSpace(password) &&
                userName.ToLower().Equals("admin") &&
                password.Equals("password"))
                return true;
            else
            {
                return false;
            }
        }

        public Token NewToken()
        {
            var token = new Token
            {
                Value = Guid.NewGuid().ToString(),
                ExpiryDate = DateTime.Now.AddDays(1)
            };
            tokenList.Add(token);
            return token;
        }

        public bool VerifyToken(string token)
        {
            if (tokenList.Any(t => t.Value == token && t.ExpiryDate > DateTime.Now))
                return true;
            return false;
        }
    }
}