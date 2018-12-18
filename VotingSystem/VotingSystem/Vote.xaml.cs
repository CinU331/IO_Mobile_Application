using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VotingSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Vote : ContentPage
    {
        private string authorization;
        private Picker votingPicker;
        private Picker candidatesPicker;
        private Label choice;

        public Vote(string token)
        {
            InitializeComponent();
            AddVotings();
            ChooseVoting();
            StackLayout stack = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Children = { votingPicker }
            };
            Content = stack;
            authorization = token;
        }
        public void ChooseVoting()
        {
            votingPicker = new Picker { Title = "Wybierz głosowanie:", TextColor = Color.Black, BackgroundColor = Color.Silver };
            votingPicker.ItemsSource = Votings;
            votingPicker.SelectedIndexChanged += VotingPicker_SelectedIndexChanged;
        }
        private void VotingPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((Picker)sender).SelectedItem.ToString() == Votings[0])
            {
                List<string> tmp = new List<string>
                {
                    "1. Jan Kowalski",
                    "2. Mariusz Nowak",
                    "3. Dariusz Flisak",
                    "4. Notariusz Waluta"
                };
                Candidates = tmp;
                List<string> tmp2 = new List<string>
                {
                    "1a",
                    "2a"
                };
                Authorizations = tmp2;
                AddLayout();
            }
            else if (((Picker)sender).SelectedItem.ToString() == Votings[1])
            {
                List<string> tmp = new List<string>
                {
                    "1. Marcin Kowalik",
                    "2. Mirosław Janys",
                    "3. Dariusz Flisak"
                };
                Candidates = tmp;
                List<string> tmp2 = new List<string>
                {
                    "1b",
                    "2b"
                };
                Authorizations = tmp2;
                AddLayout();
            }
        }
        public void AddLayout()
        {
            candidatesPicker = new Picker { Title = "Select a candidate", TextColor = Color.Black };
            candidatesPicker.ItemsSource = Candidates;
            candidatesPicker.SelectedIndexChanged += Picker_SelectedIndexChanged;
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
                Children = { choice, candidatesPicker, goToResults }
            };
            this.Content = stack;
            if (!Authorizations.Contains(authorization))
            {
                candidatesPicker.IsEnabled = false;
                choice.Text = "Już oddałeś swój głos.";
            }
        }
        private async void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((Picker)sender).SelectedItem.ToString() == string.Empty)
                return;
            string candidat = ((Picker)sender).SelectedItem.ToString();
            if (Authorizations.Contains(authorization))
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
        public List<string> Votings { get; set; }
        public List<string> Candidates { get; set; }
        public List<string> Authorizations { get; set; }
        public void AddVotings()
        {
            List<string> tmp = new List<string>
            {
                "08.12.2018r",
                "12.12.2018r"
            };
            Votings = tmp;
        }
        #endregion
    }
}