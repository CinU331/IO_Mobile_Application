using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VotingSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Authentication : ContentPage
	{
        private string login;
        private string password;
        private bool beAbleToVote;
        private string token;

        public Authentication()
        {
            InitializeComponent();
        }

        private void Login_Entered(object sender, EventArgs e)
        {
            login = ((Entry)sender).Text;
        }
        private void Password_Entered(object sender, EventArgs e)
        {
            password = ((Entry)sender).Text;
        }
        private void LogIn_Clicked(object sender, EventArgs args)
        {
            if (login != null && password != null)
            {
                Permissions();
                TryToLogIn(login, password);
                GoToVoting(beAbleToVote);
            }
        }

        private void GoToResults_Clicked(object sender, EventArgs e)
        {
            GoToResults();
        }
        public void TryToLogIn(string login, string password)
        {
            //
        }

        public void Permissions()
        {
            beAbleToVote = true;
            token = "1";
        }

        public async void GoToVoting(bool beAbleToVote)
        {
            if (beAbleToVote)
            {
                var page = new Vote();
                await Navigation.PushAsync(page);
            }
        }

        public async void GoToResults()
        {
            var page = new Results();
            await Navigation.PushAsync(page);
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