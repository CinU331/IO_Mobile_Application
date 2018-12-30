using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
        private Button reset;
        public List<Ballot> Ballots { get; set; }
        public Ballot Ballot { get; set; }
        public Candidate SelectedCandidate { get; set; }
        public List<Candidate> Candidates { get; set; }

        public Vote(string token)
        {
            InitializeComponent();
            AddBallots();
            ChooseBallot();
            StackLayout stack = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Children = { votingPicker }
            };
            Content = stack;
            authorization = token;
        }
        public void AddBallots()
        {
            Ballots = Task.Run(async () => { return await API.GetBallots(authorization); }).Result;
        }

        public void ChooseBallot()
        {
            votingPicker = new Picker { Title = "Wybierz głosowanie:", TextColor = Color.Black };
            votingPicker.ItemsSource = Ballots;
            votingPicker.SelectedIndexChanged += VotingPicker_SelectedIndexChanged;
        }
        private void VotingPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ballot = (Ballot)((Picker)sender).SelectedItem;
            Candidates = Task.Run(async () => { return await API.GetCandidateNamesForBallot(Ballot, authorization); }).Result;
            AddLayout();
            if (Ballots.Count <= 1)
                reset.IsEnabled = false;
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
            reset = new Button
            {
                Text = "Wybierz inne głosowanie",
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Center
            };
            reset.Clicked += async (sender, args) => await Navigation.PushAsync(new Vote(authorization));
            StackLayout stack = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Children = { choice, candidatesPicker, goToResults, reset }
            };
            this.Content = stack;
            /* if (!Authorizations.Contains(authorization))
             {
                 candidatesPicker.IsEnabled = false;
                 choice.Text = "Już oddałeś swój głos.";
             }*/
        }
        private async void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((Picker)sender).SelectedItem.ToString() == string.Empty)
                return;
            SelectedCandidate = (Candidate)((Picker)sender).SelectedItem;
            //if (Authorizations.Contains(authorization))
            //{
            var answer = await DisplayAlert("Potwierdzenie", "Czy na pewno chcesz oddać głos na: " + SelectedCandidate.Name + "?", "Tak", "Nie");
            if (answer)
            {
                SaveVote();
                ((Picker)sender).IsEnabled = false;
                choice.Text = "Już oddałeś głos";
            }
            else
                ((Picker)sender).SelectedItem = string.Empty;
            //}
        }

        public async void SaveVote()
        {
            await API.Vote(Ballot, SelectedCandidate, authorization);
            DisplayMessage(SelectedCandidate.Name);
        }

        public void DisplayMessage(string candidateName)
        {
            DisplayAlert("", "Dziękujemy za oddanie głosu na " + candidateName + ".", "OK");
        }

        public async void GoToResults()
        {
            await Navigation.PushAsync(new Results());
        }
    }
}