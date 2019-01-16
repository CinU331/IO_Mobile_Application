using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VotingSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Results : ContentPage
    {
        private Picker picker;
        private Image resultsImage;
        private Button reset;

        public List<Ballot> Ballots { get; set; }

        public Results()
        {
            InitializeComponent();
            AddBallots();
            ChooseBallots();
            StackLayout stack = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Children = { picker }
            };
            Content = stack;
        }

        public void AddBallots()
        {
            Ballots = Task.Run(async () => { return await API.GetBallots(); }).Result;
            for (int i = 0; i < Ballots.Count; i++)
            {
                if (Ballots[i].state == true)
                    Ballots.RemoveAt(i);
            }
        }

        public void ChooseBallots()
        {
            picker = new Picker { Title = "Wybierz głosowanie:", TextColor = Color.Black };
            picker.ItemsSource = Ballots;
            picker.SelectedIndexChanged += Picker_SelectedIndexChanged;
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetResultsInPng((Ballot)((Picker)sender).SelectedItem);
            DiplaysResults();
        }

        public void GetResultsInPng(Ballot ballot)
        {
            resultsImage = Task.Run(async () => { return await API.GetResultsImage(ballot); }).Result;
            if (resultsImage == null)
                DisplayAlert("", "Wystąpił błąd. Nie można było pobrać wyników.", "OK");
            else
                resultsImage.Aspect = Aspect.AspectFit;
        }

        public void DiplaysResults()
        {
            reset = new Button
            {
                Text = "Wybierz inne głosowanie",
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Center
            };
            reset.Clicked += async (sender, args) => await Navigation.PushAsync(new Results());
            StackLayout stack;
            if (resultsImage != null)
            {
                stack = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                    Children = { resultsImage, reset }
                };
            }
            else
            {
                stack = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                    Children = { reset }
                };
            }

            ScrollView scrollView = new ScrollView
            {
                Content = stack,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = 0
            };
            Content = scrollView;
        }
    }
}