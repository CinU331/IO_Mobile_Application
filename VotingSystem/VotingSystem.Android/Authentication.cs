using Android.App;
using Android.OS;

namespace VotingSystem.Droid
{
    [Activity(Label = "Authentication")]
    public class Authentication : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }

        private string login;
        private string password;
        private bool beAbleToVote;
        private string token;

        public void TryToLogIn(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

        public void Permissions()
        {
            beAbleToVote = true;
            token = "1";
        }

        public void GoToVoting(bool beAbleToVote)
        {
            // if (beAbleToVote)
        }

        public void GoToResults()
        {

        }

        public string GetLogin()
        {
            return login;
        }
        public string GetToken()
        {
            return token;
        }
    }
}