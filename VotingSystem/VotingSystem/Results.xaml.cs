using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VotingSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Results : ContentPage
	{
        private string attendanceImagePath;
        private string distributionOfVotesImagePath;
        private Label attendance;
        private Label distributionOfVotes;
        private Image attendanceImage;
        private Image distributionOfVotesImage;

		public Results ()
		{
			InitializeComponent ();
            attendanceImagePath = "attendance.jpg";
            GetAttendance(attendanceImagePath);
            distributionOfVotesImagePath = "distributionOfVotes.png";
            GetDistributionOfVotes(distributionOfVotesImagePath);
            DiplaysResults();
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
    }
}