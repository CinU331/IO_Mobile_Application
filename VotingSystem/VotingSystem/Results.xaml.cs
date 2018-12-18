using System;
using System.Collections.Generic;
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

		public Results ()
		{
			InitializeComponent();
            AddVotings();
            ChooseVoting();
            StackLayout stack = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Children = { picker }
            };
            Content = stack;
        }

        public void ChooseVoting()
        {
            picker = new Picker { Title = "Wybierz głosowanie:", TextColor = Color.Black, BackgroundColor = Color.Silver};
            picker.ItemsSource = Votings;
            picker.SelectedIndexChanged += Picker_SelectedIndexChanged;
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((Picker)sender).SelectedItem.ToString() == Votings[0])
            {
                attendanceImagePath = "attendance.jpg";
                GetAttendance(attendanceImagePath);
                distributionOfVotesImagePath = "distributionOfVotes.png";
                GetDistributionOfVotes(distributionOfVotesImagePath);
                DiplaysResults();
            }
            else if (((Picker)sender).SelectedItem.ToString() == Votings[1])
            {
                attendanceImagePath = "attendance2.png";
                GetAttendance(attendanceImagePath);
                distributionOfVotesImagePath = "distributionOfVotes2.jpg";
                GetDistributionOfVotes(distributionOfVotesImagePath);
                DiplaysResults();
            }
        }

        public void GetAttendance(string path)
        {
            attendance = new Label
            {
                Text = "Frekwencja"
            };
            attendanceImage = new Image { Aspect = Aspect.AspectFit };
            attendanceImage.Source = path;
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
            StackLayout stack = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Children = { attendance, attendanceImage, distributionOfVotes, distributionOfVotesImage }
            };
            ScrollView scrollView = new ScrollView
            {
                Content = stack,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = 0
            };
            Content = scrollView;
        }

        #region Mocking data
        public List<string> Votings { get; set; }
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