namespace VotingSystem
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public static User CreateUser(string login, string password)
        {
            User tmp = new User
            {
                Login = login,
                Password = password
            };
            return tmp;
        }
    }
}
