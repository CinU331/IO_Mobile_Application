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
        private string attendanceImagePath;
        private string distributionOfVotesImagePath;
        private Picker picker;
        private Label attendance;
        private Label distributionOfVotes;
        private Image attendanceImage;
        private Image distributionOfVotesImage;
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
            Ballots = Task.Run(async () => { return await API.GetBallots("xd"); }).Result;
        }

        public void ChooseBallots()
        {
            picker = new Picker { Title = "Wybierz głosowanie:", TextColor = Color.Black };
            picker.ItemsSource = Ballots;
            picker.SelectedIndexChanged += Picker_SelectedIndexChanged;
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAttendance((Ballot)((Picker)sender).SelectedItem);
            DiplaysResults();
        }

        public void GetAttendance(Ballot ballot)
        {
            attendance = new Label
            {
                Text = "Frekwencja"
            };
            attendanceImage = Task.Run(async () => { return await API.GetResultsImage(ballot); }).Result;
            attendanceImage.Aspect = Aspect.AspectFit;
        }

        public void GetDistributionOfVotes(string path)
        {
            distributionOfVotes = new Label
            {
                Text = "Rozkład"
            };
            distributionOfVotesImage = new Image { Aspect = Aspect.AspectFit };
            distributionOfVotesImage.Source = path;
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
            StackLayout stack = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                //Children = { attendance, attendanceImage, distributionOfVotes, distributionOfVotesImage, reset }
                Children = { attendance, attendanceImage, reset }
            };
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