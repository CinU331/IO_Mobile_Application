using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VotingSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Vote : ContentPage
    {
        public List<string> Candidates { get; set; }
        public List<string> Authorizations { get; set; }

        private string authorization;
        private Picker picker;
        private Label choice;

        public Vote(string token)
        {
            InitializeComponent();
            AddCandidates();
            AddAuthorizations();
            AddLayout();
            authorization = token;
            if (!Authorizations.Contains(authorization))
            {
                picker.IsEnabled = false;
                choice.Text = "Już oddałeś swój głos.";
            }
        }

        public void AddLayout()
        {
            picker = new Picker { Title = "Select a candidate", TextColor = Color.Black};
            picker.ItemsSource = Candidates;
            picker.SelectedIndexChanged += Picker_SelectedIndexChanged;
            choice = new Label
            {
                Text = "Proszę wybrać kandydata.",
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center
            };
            Button goToResults = new Button
            {
                Text = "Przejdź do wyników",
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };
            goToResults.Clicked += (sender, args) => GoToResults();
            StackLayout stack = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Children = { choice, picker, goToResults }
            };
            this.Content = stack;
        }
        private async void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((Picker)sender).SelectedItem.ToString() == string.Empty)
                return;
            string candidat = ((Picker)sender).SelectedItem.ToString();
            if(Authorizations.Contains(authorization))
            {
                var answer = await DisplayAlert("Potwierdzenie", "Czy na pewno chcesz oddać głos na: " + candidat + "?", "Tak", "Nie");
                if (answer)
                {
                    SaveVote(candidat);
                    ((Picker)sender).IsEnabled = false;
                    choice.Text = "Już oddałeś głos";
                    Authorizations.Remove(authorization);
                }
                else
                    ((Picker)sender).SelectedItem = string.Empty;
                    //((Picker)sender).SelectedItem = -1;
            }
        }

        public bool CheckIfVoiceHasAlreadyBeenGiven(string user)
        {
            return false;
        }

        public bool SaveVote(string candidat)
        {
            DisplayMessage(candidat);
            return true;
        }

        public void DisplayMessage(string candidat)
        {
            DisplayAlert("", "Dziękujemy za oddanie głosu na " + candidat + ".", "OK");
        }

        public async void GoToResults()
        {
            await Navigation.PushAsync(new Results());
        }

        #region Mocking data
        public void AddCandidates()
        {
            List<string> tmp = new List<string>
            {
                "1. Jan Kowalski",
                "2. Mariusz Nowak",
                "3. Dariusz Flisak",
                "4. Notariusz Waluta"
            };
            Candidates = tmp;
        }
        public void AddAuthorizations()
        {
            List<string> tmp = new List<string>
            {
                "1a",
                "2a"
            };
            Authorizations = tmp;
        }
        #endregion
    }
}