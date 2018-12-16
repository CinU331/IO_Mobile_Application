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

        //private Authentication authentication;

        public Vote()
        {
            InitializeComponent();
            AddCandidates();
            AddLayout();
        }

        public List<string> GetCandidatesList()
        {
            return Candidates;
        }

        public void AddCandidates()
        {
            List<string> tmp = new List<string>
            {
                "Janusz",
                "Mariusz",
                "Dariusz",
                "Sanitariusz"
            };
            tmp.Sort();
            Candidates = tmp;
        }

        public void AddLayout()
        {
            Picker picker = new Picker { Title = "Select a candidate" };
            picker.ItemsSource = Candidates;
            picker.SelectedIndexChanged += Picker_SelectedIndexChanged;
            Label choice = new Label
            {
                Text = "Proszę wybrać kandydata.",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center
            };
            Button goToResults = new Button
            {
                Text = "Przejdź do wyników",
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
        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            string candidat = ((Picker)sender).SelectedItem.ToString();
            //if(CheckIfVoiceHasAlreadyBeenGiven(user))
            SaveVote(candidat);
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
            var page = new Results();
            await Navigation.PushAsync(page);
        }
    }
}