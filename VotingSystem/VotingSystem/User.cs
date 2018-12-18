using System.Collections.Generic;

namespace VotingSystem
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public static List<User> GetUsers()
        {
            List<User> users = new List<User>
            {
                new User() {Login = "123", Password = "123", Token = "1a"},
                new User() {Login = "321", Password = "321", Token = "1b"}
            };
            return users;
        }
    }
}
