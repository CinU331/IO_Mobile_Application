using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VotingSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Authentication : ContentPage
    {
        private string login;
        private string password;

        public Authentication()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            login = null;
            loginEntry.Text = null;
            password = null;
            passwordEntry.Text = null;
        }
        public bool TryToLogIn(User user)
        {
            bool isEntitled = Task.Run(async () => { return await API.LoginAsync(user); }).Result;
            return isEntitled;
        }

        #region Changes
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
                User user = User.CreateUser(login, password);
                if (TryToLogIn(user))
                    GoToVoting(user);
                else
                    DisplayAlert("", "Nieprawidłowe dane.", "OK");
            }
            else
                DisplayAlert("", "Wprowadź login oraz hasło.", "OK");
        }

        private void GoToResults_Clicked(object sender, EventArgs e)
        {
            GoToResults();
        }
        #endregion

        #region GoTo

        public async void GoToVoting(User user)
        {
            await Navigation.PushAsync(new Vote(user.Token));
        }

        public async void GoToResults()
        {
            await Navigation.PushAsync(new Results());
        }
        #endregion
    }
}